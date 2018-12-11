using Aion.Models.WFM;
using Aion.ViewModels;
using System;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class AvlbltySummaryVm : BaseVm
    {
        public List<AvailabilityPatternView> AvlbltyCollection { get; set; }
        public List<AvailabilityPatternMissingView> AvlbltyMissing { get; set; }
        public List<AvailabilitySummaryView> AvlbltySummary { get; set; }
        public TradingHoursForAvlbltyView tradingHrs { get; set; }
        public List<AvailabilityCompletionRateView> areaCompletion { get; set; }

        public string GetName(short? a)
        {
            return Enum.GetName(typeof(DayOfWeek), a - 1).Substring(0, 3);
        }
    }
}