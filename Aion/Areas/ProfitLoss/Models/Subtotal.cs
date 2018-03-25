using Aion.Helpers;

namespace Aion.Areas.ProfitLoss.Models
{
    public class Subtotal
    {
        public int? EntryId { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? QtdActualAmount { get; set; }
        public decimal? QtdBudgetAmount { get; set; }
        public decimal? YtdActualAmount { get; set; }
        public decimal? YtdBudgetAmount { get; set; }

        public decimal? PercToBudget => VmHelpers.CalcPerc(ActualAmount, BudgetAmount);
        public decimal? QtdPercToBudget => VmHelpers.CalcPerc(QtdActualAmount, QtdBudgetAmount);
        public decimal? YtdPercToBudget => VmHelpers.CalcPerc(YtdActualAmount, YtdBudgetAmount);
    }
}