﻿using Aion.Areas.Workflow.ViewModels;
using Aion.Controllers;
using Aion.DAL.Managers;
using Aion.Models.WebMaster;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.Workflow.Controllers
{
    [Authorize]
    public class WorkflowController : BaseController
    {
        private readonly ITicketManager _ticketManager;
        private readonly int _userGroup;
        private string _userName;

        public WorkflowController()
        {
            _ticketManager = new TicketManager();

            if (System.Web.HttpContext.Current.Session["_UserName"] != null)
            {
                if (System.Web.HttpContext.Current.Session["_wfUserGroup"] == null && System.Web.HttpContext.Current.Session["_UserName"] != null)
                {
                    System.Web.HttpContext.Current.Session["_wfUserGroup"] = _ticketManager.GetUserGroup(System.Web.HttpContext.Current.Session["_UserName"].ToString());
                }

                _userGroup = (int)System.Web.HttpContext.Current.Session["_wfUserGroup"];
                _userName = System.Web.HttpContext.Current.Session["_TPCOverride"] == null ? System.Web.HttpContext.Current.Session["_UserName"].ToString() : System.Web.HttpContext.Current.Session["_TPCOverride"].ToString();
            }
        }

        // GET: Workflow/Workflow
        public async Task<ActionResult> Index()
        {
            TicketSummaryVM vm = new TicketSummaryVM();
            
            switch (_userGroup)
            {
                case 0:
                    vm.TicketCollection = mapper.Map<List<TicketStubRefView>>(await _ticketManager.GetTicketsSelf(_userName, true));
                    break;
                case 3:
                    vm.TicketCollection = mapper.Map<List<TicketStubRefView>>(await _ticketManager.GetTicketsTPC(_userName, true, int.Parse(vm.TPCSelected)));
                    vm.TPCView = true;
                    break;
                default:
                    vm.TicketCollection = mapper.Map<List<TicketStubRefView>>(await _ticketManager.GetTicketsByAuth(_userGroup, true));
                    break;
            }
            
            vm._TypeMenu = (mapper.Map<List<TicketTypeView>>(await _ticketManager.GetTypeList())).Select(x => new SelectListItem { Value = x.TicketTypeId.ToString(), Text = x.Title }).ToList();

            System.Web.HttpContext.Current.Session["_viewTicket"] = null;
            if (TempData["error"] != null)
            {
                vm.Message = TempData["error"].ToString();
                vm.MessageType = MessageType.Error;
            }

            return View(vm);
        }

        public async Task<ActionResult> ViewTicket(int TicketId)
        {
            var result = mapper.Map<TicketStubView>(await _ticketManager.GetSingleTicket(TicketId, _userGroup, _userName));

            if (result.Status == null)
            {
                TempData["error"] = "This ticket does not exist or you do not have permissions.";
                return RedirectToAction("Index");
            }
            if (_ticketManager.ChkInteractLvl(result.EscalationLevel, result.TicketTypeId) == _userGroup)
            {
                ViewBag.action = true;
            }

            System.Web.HttpContext.Current.Session["_viewTicket"] = TicketId;
            return View(result);
        }

        public async Task<ActionResult> CancelTicket(int TicketId)
        {
            if (TicketId == (int)System.Web.HttpContext.Current.Session["_viewTicket"])
            {
                var result = await _ticketManager.CancelTicket(TicketId, User.Identity.Name);
                if (result == -5)
                {
                    TempData["error"] = "You cannot cancel this ticket as you are not the owner.";
                }
            }
            else
            {
                TempData["error"] = "Ticket ID mismatch occured, please try again.";
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CompleteTicket(int TicketId)
        {
            if (TicketId == (int)System.Web.HttpContext.Current.Session["_viewTicket"])
            {
                var result = await _ticketManager.CompleteTicket(TicketId, User.Identity.Name, _userGroup);
                if (result == -5)
                {
                    TempData["error"] = "You cannot alter this ticket as it is assigned to someone else..";
                }
            }
            else
            {
                TempData["error"] = "Ticket ID mismatch occured, please try again.";
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SendTicket(int lvlAction)
        {
            var result = await _ticketManager.SendTicket((int)System.Web.HttpContext.Current.Session["_viewTicket"], User.Identity.Name, lvlAction, _userGroup);
            if (result == -5)
            {
                TempData["error"] = "You cannot alter this ticket as it is assigned to someone else.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<PartialViewResult> _UpdateSummary(string status, string type, string TPC = "")
        {
            List<TicketStubRefView> toReturn = new List<TicketStubRefView>();
            if(TPC != "")
            {
                System.Web.HttpContext.Current.Session["_TPCOverride"] = TPC;
                System.Web.HttpContext.Current.Session["_WFTPCSelect"] = TPC;
                _userName = TPC;
            }

            bool bStatus = status == "1";
            switch (_userGroup)
            {
                case 0:
                    toReturn = mapper.Map<List<TicketStubRefView>>(await _ticketManager.GetTicketsSelf(_userName, bStatus));
                    break;
                case 3:
                    toReturn = mapper.Map<List<TicketStubRefView>>(await _ticketManager.GetTicketsTPC(_userName, bStatus, int.Parse(TPC)));
                    break;
                default:
                    toReturn = mapper.Map<List<TicketStubRefView>>(await _ticketManager.GetTicketsByAuth(_userGroup, bStatus));
                    break;
            }

            return PartialView("~/Areas/Workflow/Views/Workflow/Partials/_SummaryList.cshtml", toReturn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<PartialViewResult> _PostNewComment(string commentText)
        {
            var toReturn = mapper.Map<TicketCommentView>(await _ticketManager.AddNewComment(commentText, User.Identity.Name, (int)System.Web.HttpContext.Current.Session["_viewTicket"]));
            return PartialView("~/Areas/Workflow/Views/Workflow/Partials/_NewComment.cshtml", toReturn);
        }

        [HttpGet]
        public PartialViewResult _GetEscalationOptions(int ticketType, int level)
        {
            var toReturn = mapper.Map<List<EscalationOptionsView>>(_ticketManager.GetEscalationOptions(ticketType, level));
            return PartialView("~/Areas/Workflow/Views/Workflow/Partials/_EscOptions.cshtml", toReturn);
        }

        [HttpGet]
        [OutputCache(Duration = 30, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Client, NoStore = true)]
        public async Task<int> _getTicketCount()
        {
            List<TicketStubRefView> toReturn = new List<TicketStubRefView>();
            switch (_userGroup)
            {
                case 0:
                    toReturn = mapper.Map<List<TicketStubRefView>>(await _ticketManager.GetTicketsSelf(_userName, true));
                    break;
                case 3:
                    toReturn = mapper.Map<List<TicketStubRefView>>(await _ticketManager.GetTicketsTPC(_userName, true));
                    break;
                default:
                    toReturn = mapper.Map<List<TicketStubRefView>>(await _ticketManager.GetTicketsByAuth(_userGroup, true));
                    break;
            }

            return _userGroup == 0 ? toReturn.Count(x => x.Description == "Branch") : toReturn.Count();
        }

        [HttpGet]
        public async Task<string> _getRegion(int storeNum)
        {
            return await _ticketManager.GetRegion(storeNum);
        }

        [HttpGet]
        public async Task<string> _getRegionTemp(int storeNum)
        {
            return await _ticketManager.GetRegionTemp(storeNum);
        }
    }
}