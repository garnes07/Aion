using Aion.Helpers;

namespace Aion.Models.ProfitLoss
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

        public decimal? PercToBudgetTotal => VmHelpers.CalcPercTotal(ActualAmount, BudgetAmount);
        public decimal? QtdPercToBudgetTotal => VmHelpers.CalcPercTotal(QtdActualAmount, QtdBudgetAmount);
        public decimal? YtdPercToBudgetTotal => VmHelpers.CalcPercTotal(YtdActualAmount, YtdBudgetAmount);
    }
}