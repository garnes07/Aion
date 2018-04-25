using Aion.Areas.ProfitLoss.Models;
using Aion.Areas.ProfitLoss.ViewModels.Home;
using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.ProfitLoss.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IProfitLossManager _profitLossManager;

        public HomeController()
        {
            _profitLossManager = new ProfitLossManager();
        }

        [UserFilter(MinLevel = 1)]
        public async Task<ActionResult> Index(string selectedYear = null, string selectedMonth = null, bool a = false, string select = null)
        {
            if(selectArea != "S" && !a)
            {
                return RedirectToAction("Summary", new { selectedYear = selectedYear, selectedMonth = selectedMonth, a = true });
            }

            ProfitLossVm vm = new ProfitLossVm();
            int month = selectedMonth == null ? -1 : int.Parse(selectedMonth);

            var _selectArea = select == null ? selectArea : select.Split('_')[0];
            var _selectCrit = select == null ? selectCrit : select.Split('_')[1];

            switch (_selectArea)
            {
                case "S":
                    vm.Populate(mapper.Map<List<ProfitLossView>>(
                        month == -1 ? 
                        await _profitLossManager.GetStorePandL(_selectCrit) : 
                        await _profitLossManager.GetStorePandL(_selectCrit, selectedYear, month)));
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.Populate(mapper.Map<List<ProfitLossView>>(
                        month == -1 ?
                        await _profitLossManager.GetRegionPandL(_selectCrit) :
                        await _profitLossManager.GetRegionPandL(_selectCrit, selectedYear, month)));
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.Populate(mapper.Map<List<ProfitLossView>>(
                        month == -1 ?
                        await _profitLossManager.GetDivisionPandL(_selectCrit) :
                        await _profitLossManager.GetDivisionPandL(_selectCrit, selectedYear, month)));
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.Populate(mapper.Map<List<ProfitLossView>>(
                        month == -1 ?
                        await _profitLossManager.GetChannelPandL(_selectCrit) :
                        await _profitLossManager.GetChannelPandL(_selectCrit, selectedYear, month)));
                    vm.DisplayLevel = 4;
                    break;
            }

            if(select != null)
            {
                ViewBag.back = 1;

                switch (_selectArea)
                {
                    case "S":
                        ViewBag.header = _selectCrit + " - " + vm.BreakdownLines.FirstOrDefault().ManagerName;
                        break;
                    case "R":
                        ViewBag.header = "Region " + _selectCrit;
                        break;
                    case "D":
                        ViewBag.header = "Division " + _selectCrit;
                        break;
                }
            }
            
            vm.SelectedMonth = selectedMonth == null && vm.DetailLines != null ? vm.DetailLines[0].PeriodMonth.ToString() : selectedMonth;
            vm.SelectedYear = selectedYear == null && vm.DetailLines != null ? vm.DetailLines[0].PeriodYear : selectedYear;

            return View(vm);
        }

        [UserFilter(MinLevel = 2)]
        public async Task<ActionResult> Summary(string selectedYear = null, string selectedMonth = null, bool a = false)
        {
            if (!a)
            {
                return RedirectToAction("Index", new { selectedYear = selectedYear, selectedMonth = selectedMonth });
            }

            ProfitLossSummaryVm vm = new ProfitLossSummaryVm();
            int month = selectedMonth == null ? -1 : int.Parse(selectedMonth);
            switch (selectArea)
            {
                case "R":
                    vm.collection = mapper.Map<List<ProfitLossSummaryView>>(
                        month == -1 ?
                        await _profitLossManager.GetRegionPLSummary(selectCrit) :
                        await _profitLossManager.GetRegionPLSummary(selectCrit, selectedYear, month));
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.collection = mapper.Map<List<ProfitLossSummaryView>>(
                        month == -1 ?
                        await _profitLossManager.GetDivisionPLSummary(selectCrit) :
                        await _profitLossManager.GetDivisionPLSummary(selectCrit, selectedYear, month));
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.collection = mapper.Map<List<ProfitLossSummaryView>>(
                        month == -1 ?
                        await _profitLossManager.GetChannelPLSummary(selectCrit) :
                        await _profitLossManager.GetChannelPLSummary(selectCrit, selectedYear, month));
                    vm.DisplayLevel = 4;
                    break;
            }

            ViewBag.current = selectArea == "C" ? "All" : selectCrit;
            vm.SelectedMonth = selectedMonth == null && vm.collection != null ? vm.collection[0].Month.ToString() : selectedMonth;
            vm.SelectedYear = selectedYear == null && vm.collection != null ? vm.collection[0].Year : selectedYear;

            return View(vm);
        }
    }
}