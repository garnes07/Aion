using Aion.Areas.WFM.ViewModels.ColleaguePortal;
using Aion.Controllers;
using Aion.DAL.Managers;
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
                    var person = await _empSummaryManager.GetEmployeeMatchingNumber(System.Web.HttpContext.Current.Session["_EmpNum"].ToString());
                    System.Web.HttpContext.Current.Session["_PTFlag"] = person?.EmployeeStandardHours != 45 ? "PT" : "FT";
                }
                else
                {
                    System.Web.HttpContext.Current.Session["_PTFlag"] = "";
                }
            }

            if (System.Web.HttpContext.Current.Session["_PilotFlag"] != null && (bool)System.Web.HttpContext.Current.Session["_PilotFlag"] == true)
            {
                vm.avlbltyPattern = await _avlbltyManager.GetAllPatternsPerson(System.Web.HttpContext.Current.Session["_EmpNum"].ToString());
                vm.avlbltlyPilot = true;
            }

            vm.rawMenu = await _payCalendarManager.GetPayCalendarRef(((bool)System.Web.HttpContext.Current.Session["_ROIFlag"] ? "ROI" : "CPW") + System.Web.HttpContext.Current.Session["_PTFlag"].ToString());

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
                vm.payDates = await _payCalendarManager.GetPayCalendarDates(((bool)System.Web.HttpContext.Current.Session["_ROIFlag"] ? "ROI" : "CPW") + System.Web.HttpContext.Current.Session["_PTFlag"].ToString(), period);
                vm.tSheet = await _kronosManager.GetTimesheet(vm.payDates.Select(x => x.WCDate).ToArray(), payroll, sessionID);
                vm.punch = await _clockManager.GetEmployeePunch(payroll, vm.payDates.Min(x => x.Week), vm.payDates.Max(x => x.Week));
            }
            else
            {
                vm.errorPayroll = true;
            }

            return PartialView("~/Areas/WFM/Views/ColleaguePortal/Partials/_PayData.cshtml", vm);
        }
    }
}