using Aion.Helpers;
using System;

namespace Aion.Areas.ProfitLoss.Models
{
    public class ProfitLossSummaryView
    {
        public int Heirarchy { get; set; }
        public string Year { get; set; }
        public Nullable<short> Month { get; set; }
        public string DetailName { get; set; }
        public Nullable<decimal> PeriodActual { get; set; }
        public Nullable<decimal> PeriodBudget { get; set; }
        public Nullable<decimal> QuarterActual { get; set; }
        public Nullable<decimal> QuarterBudget { get; set; }
        public Nullable<decimal> AnnualActual { get; set; }
        public Nullable<decimal> AnnualBudget { get; set; }

        public decimal? PercToBudgetTotal => VmHelpers.CalcPerc(PeriodActual, PeriodBudget);
        public decimal? QTDPercToBudgetTotal => VmHelpers.CalcPerc(QuarterActual, QuarterBudget);
        public decimal? YTDPercToBudgetTotal => VmHelpers.CalcPerc(AnnualActual, AnnualBudget);

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