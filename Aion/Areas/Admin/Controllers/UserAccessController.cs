using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
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
            return View(await _authManager.GetAllUsers());
        }

        public async Task<ActionResult> Edit(string username)
        {
            return View(await _authManager.GetAccessList(username, ""));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserAccess userDetail, string[] areas)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in areas)
                {
                    if(item != "")
                        userDetail.UserAccessAreas.Add(new UserAccessArea
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
        public async Task<ActionResult> Add(UserAccess userDetail, string[] areas)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in areas)
                {
                    if(item != "")
                    {
                        userDetail.UserAccessAreas.Add(new UserAccessArea
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