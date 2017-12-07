using Aion.App_Start;
using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Helpers;
using Aion.Models.Utils;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Aion.Models.Services
{
    public class AuthenticationService
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IAuthManager _authManager;

        public AuthenticationService(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
            _authManager = new AuthManager();
        }

        public async Task<AuthenticationResult> SignIn(string userName, string password)
        {
            AuthenticationResult authResult = new AuthenticationResult();

#if DEBUG
            authResult.UserName = "AllenJ14";
            HttpContext.Current.Session.Add("_EmpNum", "e");
            await CheckAccessLevel(authResult);
            return authResult;
#endif

            if (userName.StartsWith("DSG\\"))
            {
                userName = userName.Replace("DSG\\", "");
            }

            //try CPWPLC first
            var principalContext = new PrincipalContext(ContextType.Domain, "CPWPLC");
            bool IsAuthenticated = false;
            UserPrincipal userPrincipal = null;
            try
            {
                IsAuthenticated = principalContext.ValidateCredentials(userName, password, ContextOptions.Negotiate);
                if (IsAuthenticated)
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalContext, userName);
                    authResult.UserName = userPrincipal.SamAccountName;
                }
            }
            catch (Exception e)
            {
                IsAuthenticated = false;
                userPrincipal = null;
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            //try DSG if not authenticated via CPWPLC
            if (!IsAuthenticated)
            {
                principalContext = new PrincipalContext(ContextType.Domain, "DSG");
                try
                {
                    IsAuthenticated = principalContext.ValidateCredentials(userName, password, ContextOptions.Negotiate);
                    if (IsAuthenticated)
                    {
                        userPrincipal = UserPrincipal.FindByIdentity(principalContext, userName);
                        authResult.UserName = userPrincipal.SamAccountName;
                    }
                }
                catch (Exception e)
                {
                    IsAuthenticated = false;
                    userPrincipal = null;
                    Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                }
            }

            //Return message if authentication unsuccessful
            if (!IsAuthenticated || userPrincipal == null)
            {
                authResult.ErrorMessage = "Username or Password is not correct";
                return authResult;
            }
            
            //create identity and sign in
            var identity = CreateIdentity(userPrincipal);
            _authenticationManager.SignOut(AionAuthentication.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

            //Get employee number and translate as per business context
            var entry = new DirectoryEntry("LDAP://" + principalContext.ConnectedServer + "/" + userPrincipal.DistinguishedName, userName, password);
            if (principalContext.Name != "DSG")
            {
                RetriveCPWPLCEmpNum(entry);
            }
            else
            {
                RetriveDSGEmpNum(entry);
            }

            await CheckAccessLevel(authResult);
            await _authManager.RecordLogIn(authResult.UserName);

            return authResult;
        }

        //Extract CPWPLC emp num form AD and translate to business context
        private void RetriveCPWPLCEmpNum(DirectoryEntry entry)
        {
            try
            {
                string empNum = entry.Properties["employeeNumber"].Value.ToString();
                if (!(bool)HttpContext.Current.Session["_ROIFlag"])
                {
                    HttpContext.Current.Session.Add("_EmpNum", empNum == "" ? "e" : "UK" + empNum.PadLeft(6, '0'));
                }
                else
                {
                    HttpContext.Current.Session.Add("_EmpNum", empNum == "" ? "e" : empNum);
                }
            }
            catch(Exception e)
            {
                HttpContext.Current.Session.Add("_EmpNum", "e");
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return;
        }

        //Extract DSG emp num form AD and translate to business context
        private void RetriveDSGEmpNum(DirectoryEntry entry)
        {
            try
            {
                string empNum = entry.Properties["workforceID"].Value.ToString();
                HttpContext.Current.Session.Add("_EmpNum", "DSG" + empNum);
            }
            catch(Exception e)
            {
                HttpContext.Current.Session.Add("_EmpNum", "e");
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return;
        }

        //Create and return claim
        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(AionAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
            if (!string.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }

            return identity;
        }

        //Check access level from WebMaster
        public async Task<bool> CheckAccessLevel(AuthenticationResult a)
        {
            var UserAccess = await _authManager.GetAccessList(a.UserName, a.EmpNum);
            if (UserAccess.Count == 0)
            {
                HttpContext.Current.Session["_AccessLevel"] = "0";
                return false;
            }
            else
            {
                HttpContext.Current.Session["_AccessLevel"] = UserAccess.First().AccessLevel;
                var result = await LoadStoreMenu(UserAccess.First().AccessLevel, UserAccess.Select(x => x.Area).ToArray());
                return true;
            }
        }

        public static async Task<bool> LoadStoreMenu(byte accessLevel, string[] accessArea)
        {
            StoreManager _storeManager = new StoreManager();
            List<StoreMaster> menuList = new List<StoreMaster>();
            string _default = "";

            if (accessLevel <= 1)
            {
                menuList = await _storeManager.GetStoreMenu(short.Parse(accessArea[0]));
                _default = "S_" + accessArea[0];
            }
            else if (accessLevel == 2)
            {
                menuList = await _storeManager.GetRegionMenu(Array.ConvertAll(accessArea, s => short.Parse(s)));
                _default = "R_" + accessArea[0];
            }
            else if (accessLevel == 3)
            {
                menuList = await _storeManager.GetDivisionMenu(accessArea[0]);
                _default = "D_" + accessArea[0];
            }
            else
            {
                if (accessArea[0] != "All")
                {
                    menuList = await _storeManager.GetDivisionMenu(accessArea[0]);
                }
                else
                {
                    menuList = await _storeManager.GetAllMenu();
                }
                _default = "C_" + (accessArea[0] != "All" ? accessArea[0] : "SAS");
            }

            StoreMenu _menu = new StoreMenu(menuList, _default, accessLevel);

            HttpContext.Current.Session["_storeMenu"] = _menu;
            HttpContext.Current.Session["_store"] = menuList.First();
            HttpContext.Current.Session["_ROIFlag"] = menuList.First().Chain == "ROI";

            return true;
        }
    }
}