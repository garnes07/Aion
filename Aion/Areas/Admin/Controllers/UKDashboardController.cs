using Aion.Attributes;
using Aion.Models.Shared;
using Aion.DAL.Managers;
using PagedList;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Aion.Areas.Admin.ViewModels.Shared;
using Aion.Controllers;
using System.Collections.Generic;

namespace Aion.Areas.Admin.Controllers
{
    [UserFilter(MinLevel = 9)]
    public class UKDashboardController : BaseController
    {
        private readonly IAdminDashManager _dashManager;

        public UKDashboardController()
        {
            _dashManager = new UKDashManager();
        }

        public async Task<ActionResult> Index()
        {
            if (TempData["PackageRunning"] != null)
            {
                ViewBag.PackageRunning = true;
            }

            DashboardErrors vm = new DashboardErrors();
            vm.RoleErrors = await _dashManager.GetRoleErrors();
            vm.StoreErrors = await _dashManager.GetStoreErrors();

            return View(vm);
        }

        //Run sp to build dashboard with existing timecard data file
        public ActionResult RunBuild()
        {
            TempData["PackageRunning"] = _dashManager.RunBuild();

            return RedirectToAction("Index");
        }

        //Run sp to build dashboard with new timecard data file
        public ActionResult RunBuildNewData()
        {
            TempData["PackageRunning"] = _dashManager.RunBuildNewData();
            return RedirectToAction("Index");
        }

        //Run sp to import and update budgets from file
        public ActionResult RunBudgets()
        {
            TempData["PackageRunning"] = _dashManager.RunBudgets();
            return RedirectToAction("Index");
        }

        //Run sp to import and update budgets from file
        public ActionResult PushData()
        {
            TempData["PackageRunning"] = _dashManager.PushData();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> StoreList(int? page, string searchString)
        {
            var result = mapper.Map<List<StoreReferenceView>>(await _dashManager.StoreReferenceList());

            if (!string.IsNullOrEmpty(searchString))
            {
                int numVal = int.Parse(searchString);
                result = result.Where(x => x.Br_ == numVal).ToList();
            }

            int pageSize = 50;
            int pageNumber = page ?? 1;

            return View(result.ToPagedList(pageNumber, pageSize));
        }

        //add new storereference record
        public ActionResult AddStore(string SeedNumber)
        {
            ViewBag.SeedNumber = SeedNumber;
            return View();
        }

        //post new storereference record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStore(StoreReferenceView model)
        {
            if (ModelState.IsValid)
            {
                await _dashManager.SubmitNewStoreReference(model);
            }            
            return RedirectToAction("StoreList");
        }

        //get storereference record for edit
        public async Task<ActionResult> EditStore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StoreReferenceView storeReference = mapper.Map<StoreReferenceView>(await _dashManager.StoreReferenceSingle(id));
            if (storeReference == null)
            {
                return HttpNotFound();
            }

            return View(storeReference);
        }

        //post changes to storereference record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditStore(StoreReferenceView model)
        {
            if (ModelState.IsValid)
            {
                await _dashManager.SubmitStoreReferenceChange(model);
            }            
            return RedirectToAction("Index");
        }

        //get storereference record for confirm delete
        public async Task<ActionResult> DeleteStore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StoreReferenceView storeReference = mapper.Map<StoreReferenceView>(await _dashManager.StoreReferenceSingle(id));
            if (storeReference == null)
            {
                return HttpNotFound();
            }

            return View(storeReference);
        }

        //post delete storereference record
        [HttpPost, ActionName("DeleteStore")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteStoreConfirmed(int id)
        {
            await _dashManager.DeleteStoreReferenceRecord(id);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RoleList()
        {
            return View(mapper.Map<List<RoleReferenceView>>(await _dashManager.RoleReferenceList()));
        }

        //add new rolereference record
        public ActionResult AddRole(string SeedRole)
        {
            ViewBag.SeedRole = SeedRole;
            return View();
        }

        //post new rolereference record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRole(RoleReferenceView model)
        {
            if (ModelState.IsValid)
            {
                await _dashManager.SubmitNewRoleReference(model);
            }            
            return RedirectToAction("RoleList");
        }

        //get rolereference record for edit
        public async Task<ActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoleReferenceView roleReference = mapper.Map<RoleReferenceView>(await _dashManager.RoleReferenceSingle(id));
            if (roleReference == null)
            {
                return HttpNotFound();
            }

            return View(roleReference);
        }

        //post changes to rolereference record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRole(RoleReferenceView model)
        {
            if (ModelState.IsValid)
            {
                await _dashManager.SubmitRoleReferenceChange(model);
            }
            return RedirectToAction("RoleList");
        }

        //get rolereference record for confirm delete
        public async Task<ActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoleReferenceView roleReference = mapper.Map<RoleReferenceView>(await _dashManager.RoleReferenceSingle(id));
            if (roleReference == null)
            {
                return HttpNotFound();
            }

            return View(roleReference);
        }

        //post delete rolereference record
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRoleConfirmed(string id)
        {
            await _dashManager.DeleteRoleReferenceRecord(id);

            return RedirectToAction("Index");
        }
    }
}