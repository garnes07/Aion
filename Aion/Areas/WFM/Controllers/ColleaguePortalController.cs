using Aion.Areas.WFM.ViewModels.ColleaguePortal;
using Aion.Controllers;
using Aion.DAL.Managers;
using Aion.Models.WFM;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.WFM.Controllers
{
    public class ColleaguePortalController : BaseController
    {
        private readonly IEmpSummaryManager _empSummaryManager;
        private readonly IAvlbltyManager _avlbltyManager;
        private readonly IPayCalendarManager _payCalendarManager;
        private readonly IKronosManager _kronosManager;
        private readonly IClockingDataManager _clockManager;

        public ColleaguePortalController()
        {
            _empSummaryManager = new EmpSummaryManager();
            _avlbltyManager = new AvlbltyManager();
            _payCalendarManager = new PayCalendarManager();
            _kronosManager = new KronosManager();
            _clockManager = new ClockingDataManager();
        }

        [Authorize]
        public async Task<ActionResult> ColleaguePortal()
        {
            ColleaguePortalVm vm = new ColleaguePortalVm();

            if (System.Web.HttpContext.Current.Session["_PTFlag"] == null)
            {
                if (!(bool)System.Web.HttpContext.Current.Session["_ROIFlag"])
                {
                    var empNum = System.Web.HttpContext.Current.Session["_EmpNum"].ToString();
                    var person = mapper.Map<KronosEmployeeSummaryView>(await _empSummaryManager.GetEmployeeMatchingNumber(empNum == "" ? "e" : "UK" + empNum.PadLeft(6, '0')));
                    System.Web.HttpContext.Current.Session["_PTFlag"] = "PT";//person?.EmployeeStandardHours != 45 ? "PT" : "FT";
                }
                else
                {
                    System.Web.HttpContext.Current.Session["_PTFlag"] = "";
                }
            }

            if (System.Web.HttpContext.Current.Session["_PilotFlag"] != null && (bool)System.Web.HttpContext.Current.Session["_PilotFlag"] == true)
            {
                var empNum = System.Web.HttpContext.Current.Session["_EmpNum"].ToString();
                if (!(bool)System.Web.HttpContext.Current.Session["_ROIFlag"])
                {
                    empNum = "UK" + empNum.PadLeft(6, '0');
                }
                vm.avlbltyPattern = mapper.Map<List<AvailabilityPatternView>>(await _avlbltyManager.GetAllPatternsPerson(empNum));
                vm.avlbltlyPilot = true;
            }

            vm.rawMenu = mapper.Map<List<PayCalendarRefView>>(await _payCalendarManager.GetPayCalendarRef(((bool)System.Web.HttpContext.Current.Session["_ROIFlag"] ? "ROI" : "CPW") + System.Web.HttpContext.Current.Session["_PTFlag"].ToString()));

            return View(vm);
        }

        [Authorize]
        public async Task<PartialViewResult> _PayData(string period)
        {
            ColleaguePayDataVm vm = new ColleaguePayDataVm();

            string payroll = System.Web.HttpContext.Current.Session["_EmpNum"].ToString();
            string sessionID = System.Web.HttpContext.Current.Session.SessionID;

            if (payroll != "e")
            {
                if (!(bool)System.Web.HttpContext.Current.Session["_ROIFlag"])
                {
                    payroll = "UK" + payroll.PadLeft(6, '0');
                }
                vm.payDates = mapper.Map<List<PayCalendarDateView>>(await _payCalendarManager.GetPayCalendarDates(((bool)System.Web.HttpContext.Current.Session["_ROIFlag"] ? "ROI" : "CPW") + System.Web.HttpContext.Current.Session["_PTFlag"].ToString(), period));
                vm.tSheet = await _kronosManager.GetTimesheet(vm.payDates.Select(x => x.WCDate).ToArray(), payroll, sessionID);
                vm.punch = mapper.Map<List<CPW_Clocking_DataView>>(await _clockManager.GetEmployeePunch(payroll, vm.payDates.Min(x => x.Week), vm.payDates.Max(x => x.Week)));
            }
            else
            {
                vm.errorPayroll = true;
            }

            return PartialView("~/Areas/WFM/Views/ColleaguePortal/Partials/_PayData.cshtml", vm);
        }
    }
}