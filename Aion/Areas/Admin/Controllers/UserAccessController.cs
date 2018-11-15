using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Models.UserAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.Admin.Controllers
{
    [UserFilter(MinLevel = 9)]
    public class UserAccessController : BaseController
    {
        private readonly IAuthManager _authManager;

        public UserAccessController()
        {
            _authManager = new AuthManager();
        }

        public async Task<ActionResult> Index()
        {
            return View(mapper.Map<List<UserAccessView>>(await _authManager.GetAllUsers()));
        }

        public async Task<ActionResult> Edit(string username)
        {
            return View(mapper.Map<UserAccessView>(await _authManager.GetAccessList(username, "")));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserAccessView userDetail, string[] areas)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in areas)
                {
                    if(item != "")
                        userDetail.UserAccessAreas.Add(new UserAccessAreaView
                        {
                            AreaName = item,
                            UserName = userDetail.UserName
                        });
                }
                userDetail.Krn = false;
                await _authManager.EditUser(userDetail);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(UserAccessView userDetail, string[] areas)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in areas)
                {
                    if(item != "")
                    {
                        userDetail.UserAccessAreas.Add(new UserAccessAreaView
                        {
                            AreaName = item
                        });
                    }                    
                }
                userDetail.Krn = false;
                await _authManager.AddNewUserRecord(userDetail);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string username)
        {
            await _authManager.DeleteUser(username);
            return RedirectToAction("Index");
        }
    }
}