using Aion.DAL.Entities;
using Aion.ViewModels;
using System;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class AvlbltySummaryVm : BaseVm
    {
        public List<vw_AvailabilityPattern> AvlbltyCollection { get; set; }
        public List<vw_AvailabilityPatternMissing> AvlbltyMissing { get; set; }
        public List<vw_AvailabilitySummary> AvlbltySummary { get; set; }

        public string GetName(short? a)
        {
            return Enum.GetName(typeof(DayOfWeek), a - 1).Substring(0, 3);
        }
    }
}