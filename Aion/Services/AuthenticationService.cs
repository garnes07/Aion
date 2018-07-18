using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Aion.App_Start;
using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Helpers;
using Aion.Models.Utils;
using Microsoft.Owin.Security;

namespace Aion.Services
{
    public class AuthenticationService
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IAuthManager _authManager;
        private readonly IStoreManager _storeManager;
        private readonly ITicketManager _ticketManager;

        public AuthenticationService(IAuthenticationManager authenticationManager = null)
        {
            _authenticationManager = authenticationManager;
            _authManager = new AuthManager();
            _storeManager = new StoreManager();
            _ticketManager = new TicketManager();
        }

        public async Task<AuthenticationResult> SignIn(string userName, string password)
        {
            AuthenticationResult authResult = new AuthenticationResult();
            string Domain = "DSG";

            if (userName.StartsWith("DSG\\", true, new System.Globalization.CultureInfo("en-US")))
            {
                //userName = userName.Replace("DSG\\", "");
                userName = System.Text.RegularExpressions.Regex.Replace(userName, "([Dd][Ss][Gg]\\\\)", "");
                Domain = "DSG";
            }

            //try CPWPLC first
            var principalContext = new PrincipalContext(ContextType.Domain, Domain);
            bool IsAuthenticated;
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
                Domain = Domain == "CPWPLC" ? "DSG" : "CPWPLC";
                principalContext = new PrincipalContext(ContextType.Domain, Domain);
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
            if (!IsAuthenticated)
            {
                authResult.ErrorMessage = "Username or Password is not correct";
                return authResult;
            }
            
            //create identity and sign in
            var identity = CreateIdentity(userPrincipal);
            _authenticationManager.SignOut(AionAuthentication.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

            //Get employee number and translate as per business context
            var entry = new DirectoryEntry("LDAP://" + principalContext.ConnectedServer + "/" + userPrincipal.DistinguishedName, userName, password);
            RetrieveEmpNum(entry);
            //if (principalContext.Name != "DSG")
            //{
            //    RetriveCPWPLCEmpNum(entry);
            //}
            //else
            //{
            //    RetriveDSGEmpNum(entry);
            //}
            
            if (HttpContext.Current.Session["_wfUserGroup"] == null)
            {
                HttpContext.Current.Session["_wfUserGroup"] = _ticketManager.GetUserGroup(authResult.UserName);
            }

            HttpContext.Current.Session["Email"] = userPrincipal.EmailAddress;

            await CheckAccessLevel(authResult);

#if DEBUG
            HttpContext.Current.Session.Add("_LoginID", 0);
#else
            var loginID = await _authManager.RecordLogIn(new UserLog
            {
                UserName = authResult.UserName,
                Timestamp = DateTime.Now,
                EmpNum = authResult.EmpNum,
                AccessLvl = authResult.AccessLevel,
                AreaLevel = authResult.AreaLevel,
                IP = MvcHelper.GetIPHelper()
            });

            HttpContext.Current.Session.Add("_LoginID", loginID);
#endif

            return authResult;
        }

        private void RetrieveEmpNum(DirectoryEntry entry)
        {
            try
            {
                if (entry.Properties.Contains("employeeNumber"))
                {
                    string empNum = entry.Properties["employeeNumber"].Value.ToString();
                    var roiFlag = HttpContext.Current.Session["_ROIFlag"] == null ? false : (bool)HttpContext.Current.Session["_ROIFlag"];
                    if (!roiFlag)
                    {
                        HttpContext.Current.Session.Add("_EmpNum", empNum);
                    }
                    else
                    {
                        var result = CheckForRemap(empNum);
                        if (result.Equals("none"))
                        {
                            HttpContext.Current.Session.Add("_EmpNum", empNum == "" ? "e" : empNum);
                        }
                        else
                        {
                            HttpContext.Current.Session.Add("_EmpNum", result);
                        }
                    }
                }
                else if (entry.Properties.Contains("dcgWorkforceID"))
                {
                    string empNum = entry.Properties["dcgWorkforceID"].Value.ToString().Replace('A', '1').Replace('B', '2').Replace('C', '3');
                    HttpContext.Current.Session.Add("_EmpNum", empNum);
                }
                else
                {
                    HttpContext.Current.Session.Add("_EmpNum", "e");
                }
            }
            catch (Exception e)
            {
                HttpContext.Current.Session.Add("_EmpNum", "e");
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
        }

        //Extract CPWPLC emp num form AD and translate to business context
        private void RetriveCPWPLCEmpNum(DirectoryEntry entry)
        {
            try
            {
                string empNum = entry.Properties["employeeNumber"].Value.ToString();
                var roiFlag = HttpContext.Current.Session["_ROIFlag"] == null ? false : (bool)HttpContext.Current.Session["_ROIFlag"];
                if (!roiFlag)
                {
                    HttpContext.Current.Session.Add("_EmpNum", empNum);
                }
                else
                {
                    var result = CheckForRemap(empNum);
                    if(result.Equals("none"))
                    {
                        HttpContext.Current.Session.Add("_EmpNum", empNum == "" ? "e" : empNum);
                    }
                    else
                    {
                        HttpContext.Current.Session.Add("_EmpNum", result);
                    }                    
                }
            }
            catch(Exception e)
            {
                HttpContext.Current.Session.Add("_EmpNum", "e");
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
        }

        //Extract DSG emp num form AD and translate to business context
        private static void RetriveDSGEmpNum(DirectoryEntry entry)
        {
            try
            {
                string empNum = entry.Properties["workforceID"].Value.ToString().Replace('A', '1').Replace('B', '2').Replace('C', '3');
                HttpContext.Current.Session.Add("_EmpNum", empNum);
            }
            catch(Exception e)
            {
                HttpContext.Current.Session.Add("_EmpNum", "e");
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return;
        }

        //Create and return claim
        private static ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
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

        //Check for ROI remap
        public string CheckForRemap(string payrollNum)
        {
            var result = _authManager.CheckROIRemap(payrollNum);
            return result == null ? "none" : result.Kronos_ID;
        }

        //Check access level from WebMaster
        public async Task<bool> CheckAccessLevel(AuthenticationResult a)
        {
            var UserAccess = await _authManager.GetAccessList(a.UserName, a.EmpNum);
            if (UserAccess == null)
            {
                HttpContext.Current.Session["_AccessLevel"] = (byte)0;
                HttpContext.Current.Session["_AreaLevel"] = (byte)0;
                return false;
            }

            HttpContext.Current.Session["_AccessLevel"] = UserAccess.AccessLevel;
            HttpContext.Current.Session["_AreaLevel"] = UserAccess.AreaLevel;
            return await LoadStoreMenu(UserAccess.AreaLevel, UserAccess.UserAccessAreas.Select(x => x.AreaName).ToArray());
        }

        public async Task<bool> LoadStoreMenu(byte accessLevel, string[] accessArea)
        {            
            List<StoreMaster> menuList;
            string _default;

            if (accessLevel == 1)
            {
                string ip = MvcHelper.GetIPHelper();
#if DEBUG
                //string ipBase = "10.224.240";
                string ipBase = "10.225.56";
#else
                string ipBase = ip.Substring(0, ip.LastIndexOf("."));
#endif
                menuList = await _storeManager.GetStoreMenu(Array.ConvertAll(accessArea, short.Parse), ipBase);
                _default = "S_" + (accessArea.Length == 0 ? menuList.First().StoreNumber.ToString() : accessArea[0]);
            }
            else if (accessLevel == 2)
            {
                menuList = await _storeManager.GetRegionMenu(Array.ConvertAll(accessArea, short.Parse));
                _default = "R_" + accessArea[0];
            }
            else if (accessLevel == 3)
            {
                menuList = await _storeManager.GetDivisionMenu(accessArea);
                _default = "D_" + accessArea[0];
            }
            else if (accessLevel == 4)
            {
                menuList = await _storeManager.GetChainMenu(accessArea);
                _default = "C_" + accessArea[0];
            }
            else
            {
                menuList = await _storeManager.GetAllMenu();
                _default = "C_" + (accessArea.Any() ? accessArea[0] : "SAS");
            }

            StoreMenu _menu = new StoreMenu(menuList, _default, accessLevel);

            HttpContext.Current.Session["_storeMenu"] = _menu;
            //HttpContext.Current.Session["_store"] = menuList.First();
            HttpContext.Current.Session["_ROIFlag"] = menuList.First().Chain == "ROI" && accessLevel < 5;

            return true;
        }

        public async Task<bool> RegisterUnknownStore(int _storeNumber)
        {
            return await _authManager.RegisterStore(
                new UnknownIpLog
                {
                    storeNumber = _storeNumber,
                    IpRange = MvcHelper.GetIPHelper(),
                    DateTimeAdded = DateTime.Now
                });
        }
        
        public async Task<List<StoreMaster>> AllStoresMatchingIp()
        {
            var ip = MvcHelper.GetIPHelper();
            ip = ip.Substring(0, ip.LastIndexOf("."));
            return await _storeManager.GetStoreDetails(ip);
        }

        public async Task<bool> RegisterStoreFullIP(short _storeNumber)
        {
            return await _authManager.RegisterStoreFullIP(
                new IpRef
                {
                    IpRange = MvcHelper.GetIPHelper(),
                    StoreNumber = _storeNumber,
                    Added = DateTime.Now
                });
        }
    }
}