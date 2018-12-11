using Aion.Areas.Workflow.Models;
using Aion.Areas.Workflow.ViewModels;
using Aion.Controllers;
using Aion.DAL.Managers;
using Aion.Helpers;
using Aion.Models.WebMaster;
using Aion.Models.WFM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.Workflow.Controllers
{
    [Authorize]
    public class FormController : BaseController
    {
        private readonly ITicketManager _ticketManager;
        private readonly IHRDataManager _hrDataManager;

        public FormController()
        {
            _ticketManager = new TicketManager();
            _hrDataManager = new HRDataManager();
        }

        public async Task<ActionResult> Index()
        {
            if (TempData["ticketID"] != null)
            {
                ViewBag.ticketID = TempData["ticketID"];
            }
            if (TempData["invalidSelect"] != null)
            {
                ViewBag.invalid = TempData["invalidSelect"];
            }

            if (selectArea != "S")
            {
                ViewBag.errorMessage = "Please select a store from the top menu in order to submit a form.";
                return View();
            }
            else
            {
                return View(mapper.Map<List<TicketTypeView>>(await _ticketManager.GetTypeList()));
            }
        }

        public async Task<ActionResult> NewSubmission(int FormTypeId = -5)
        {
            if(FormTypeId == -5)
            {
                return RedirectToAction("Index");
            }
            var data = mapper.Map<List<TicketTemplateView>>(await _ticketManager.GetFormTemplate(FormTypeId));
            if(data.Count == 0)
            {
                TempData["invalidSelect"] = "The selected form does not exist.";
                return RedirectToAction("Index");
            }

            var name = await _ticketManager.GetFormName(FormTypeId);

            ViewBag.defaultEmp = TempData["empId"] != null ? TempData["empId"].ToString() : "e";

            return View(new NewSubmissionVm
            {
                QuestionList = data,
                FormName = name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewSubmission(List<TicketQ_A> q, int TicketTypeId, int exception)
        {
            if (selectArea != "S") return RedirectToAction("NewSubmission");
            foreach (var item in q.Where(x => x.ReturnType == "date"))
            {
                DateTime a = new DateTime();
                if (DateTime.TryParse(item.Answer, out a))
                {
                    item.Answer = a.ToString("yyyy-MM-dd");
                }
            }

            var toReturn = await _ticketManager.SubmitTicket(TicketTypeId, System.Web.HttpContext.Current.Session["_UserName"].ToString(), selectCrit, q, exception);

            TempData["ticketID"] = toReturn;
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<PartialViewResult> _empList()
        {
            var data = mapper.Map<List<WFMEmployeeInfoView>>(await _hrDataManager.GetStaffListStore(selectCrit));
            return PartialView("~/Areas/Workflow/Views/Form/Partials/_empList.cshtml", data);
        }

        [HttpGet]
        public PartialViewResult _timeList()
        {
            IEnumerable<SelectListItem> Times = VmHelpers.GetTimes();
            return PartialView("~/Areas/Workflow/Views/Form/Partials/_timeList.cshtml", Times.Where(x => x.Text != "Closed"));
        }

        [HttpGet]
        public async Task<PartialViewResult> _branchValidate(string storeNum)
        {
            var data = mapper.Map<StoreMasterView>(await _ticketManager.GetStore(storeNum));
            return PartialView("~/Areas/Workflow/Views/Form/Partials/_branchValidate.cshtml", data);
        }
    }
}