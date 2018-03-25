using Aion.Areas.ProfitLoss.Models;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Aion.Areas.ProfitLoss.ViewModels.Home
{
    public class ProfitLossVm : BaseVm
    {
        public string SelectedYear { get; set; }
        public string SelectedMonth { get; set; }
        public string PeriodMth { get; set; }
        public string PeriodYr { get; set; }

        public List<ProfitLossView> BreakdownLines;
        public List<ProfitLossView> DetailLines;
        public List<Subtotal> EntryTypeSubTotals;
        public List<Subtotal> EntrySubTypeSubTotals;
        public Subtotal SheetTotal;

        public List<ProfitLossView> MarginLines => DetailLines.Where(x => x.AccountEntrySubTypeId == 1).ToList();
        public List<ProfitLossView> PayrollLines => DetailLines.Where(x => x.AccountEntrySubTypeId == 2).ToList();
        public List<ProfitLossView> CostLines => DetailLines.Where(x => x.AccountEntrySubTypeId == 3).ToList();
        public List<ProfitLossView> ReductionLines => DetailLines.Where(x => x.AccountEntrySubTypeId == 5).ToList();

        public List<long> BreakdownList => DetailLines.Where(x => x.AccountEntryDetailBreakDownId != null).GroupBy(x => x.AccountEntryDetailId).Select(x => x.Key).ToList();

        public void Populate(List<ProfitLossView> collection)
        {
            PeriodMth = collection.FirstOrDefault()?.PeriodMonth.ToString();
            PeriodYr = collection.FirstOrDefault()?.PeriodYear;

            BreakdownLines = collection;

            DetailLines = collection
                    .GroupBy(x => x.AccountEntryDetailId)
                    .Select(x => x.FirstOrDefault())
                    .OrderBy(x => x.AccountEntrySubTypeId)
                    .ThenBy(x => x.AccountEntryDetailId)
                    .ThenBy(x => x.AccountEntrySubTypeText)
                    .ToList();

            EntrySubTypeSubTotals = DetailLines
                .GroupBy(x => x.AccountEntrySubTypeId)
                .Select(x => new Subtotal
                {
                    EntryId = x.Key,
                    ActualAmount = x.Sum(y => y.ActualAmount),
                    BudgetAmount = x.Sum(y => y.BudgetAmount),
                    QtdActualAmount = x.Sum(y => y.QtdActualAmount),
                    QtdBudgetAmount = x.Sum(y => y.QtdBudgetAmount),
                    YtdActualAmount = x.Sum(y => y.YtdActualAmount),
                    YtdBudgetAmount = x.Sum(y => y.YtdBudgetAmount)
                })
                .OrderBy(x => x.EntryId)
                .ToList();

            EntryTypeSubTotals = DetailLines
                .GroupBy(x => x.AccountEntryTypeId)
                .Select(x => new Subtotal
                {
                    EntryId = x.Key,
                    ActualAmount = x.Sum(y => y.ActualAmount),
                    BudgetAmount = x.Sum(y => y.BudgetAmount),
                    QtdActualAmount = x.Sum(y => y.QtdActualAmount),
                    QtdBudgetAmount = x.Sum(y => y.QtdBudgetAmount),
                    YtdActualAmount = x.Sum(y => y.YtdActualAmount),
                    YtdBudgetAmount = x.Sum(y => y.YtdBudgetAmount)
                })
                .OrderBy(x => x.EntryId)
                .ToList();

            SheetTotal = DetailLines
                .GroupBy(x => x.StoreNumber)
                .Select(x => new Subtotal
                {
                    EntryId = 1,
                    ActualAmount = x.Sum(y => y.ActualAmount),
                    BudgetAmount = x.Sum(y => y.BudgetAmount),
                    QtdActualAmount = x.Sum(y => y.QtdActualAmount),
                    QtdBudgetAmount = x.Sum(y => y.QtdBudgetAmount),
                    YtdActualAmount = x.Sum(y => y.YtdActualAmount),
                    YtdBudgetAmount = x.Sum(y => y.YtdBudgetAmount)
                })
                .OrderBy(x => x.EntryId)
                .Single();
        }


        public List<SelectListItem> PandLYears
        {
            get
            {
                var ls = new List<SelectListItem>();
                for (var i = 0; i < 4; i++)
                {
                    var yr = 2016 + i;
                    var yrTxt = (yr - 2000).ToString() + "/" + (yr - 1999).ToString();
                    ls.Add(new SelectListItem { Value = (yr + 1).ToString(), Text = yrTxt });
                }
                ls.ForEach(x => x.Selected = x.Value == PeriodYr);
                return ls;
            }
        }

        public List<SelectListItem> PandLMonths
        {
            get
            {
                var ls = new List<SelectListItem>();
                ls.Add(new SelectListItem { Value = "1", Text = "May (4 wks)" });
                ls.Add(new SelectListItem { Value = "2", Text = "June (4 wks)" });
                ls.Add(new SelectListItem { Value = "3", Text = "July (5 wks)" });
                ls.Add(new SelectListItem { Value = "4", Text = "August (4 wks)" });
                ls.Add(new SelectListItem { Value = "5", Text = "September (4 wks)" });
                ls.Add(new SelectListItem { Value = "6", Text = "October (5 wks)" });
                ls.Add(new SelectListItem { Value = "7", Text = "November (4 wks)" });
                ls.Add(new SelectListItem { Value = "8", Text = "December (5 wks)" });
                ls.Add(new SelectListItem { Value = "9", Text = "January (4 wks)" });
                ls.Add(new SelectListItem { Value = "10", Text = "February (4 wks)" });
                ls.Add(new SelectListItem { Value = "11", Text = "March (4 wks)" });
                ls.Add(new SelectListItem { Value = "12", Text = "April (5 wks)" });

                ls.ForEach(x => x.Selected = x.Value == PeriodMth);
                return ls;
            }
        }
    }
}