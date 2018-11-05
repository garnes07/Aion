using Aion.Helpers;

namespace Aion.Models.ProfitLoss
{
    public class ProfitLossSummaryView
    {
        public int Heirarchy { get; set; }
        public string Year { get; set; }
        public short? Month { get; set; }
        public string DetailName { get; set; }
        public decimal? PeriodActual { get; set; }
        public decimal? PeriodBudget { get; set; }
        public decimal? QuarterActual { get; set; }
        public decimal? QuarterBudget { get; set; }
        public decimal? AnnualActual { get; set; }
        public decimal? AnnualBudget { get; set; }

        public decimal? PercToBudgetTotal => VmHelpers.CalcPercTotal(PeriodActual, PeriodBudget);
        public decimal? QTDPercToBudgetTotal => VmHelpers.CalcPercTotal(QuarterActual, QuarterBudget);
        public decimal? YTDPercToBudgetTotal => VmHelpers.CalcPercTotal(AnnualActual, AnnualBudget);

        public string selectCrit
        {
            get
            {
                switch (Heirarchy)
                {
                    case (1):
                        return "S_" + DetailName.Substring(0, DetailName.IndexOf(' ', 0)); ;
                    case (2):
                        return "R_" + DetailName;
                    case (3):
                        return "D_" + DetailName;
                    case (4):
                        return "C_" + DetailName;
                    default:
                        return "";
                }
            }
        }
    }
}