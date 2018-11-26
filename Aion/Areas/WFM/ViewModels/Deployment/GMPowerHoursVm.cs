using Aion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Aion.Models.WFM;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class GMPowerHoursVm : BaseVm
    {
        public List<GMPowerHourView> collection { get; set; }

        public double complianceScore => (collection.Where(x => x.Rank <= 20).Sum(x => x.Working) / 20.0 * 100);

        public string GetName(int a)
        {
            return Enum.GetName(typeof(DayOfWeek), a - 1).Substring(0, 3);
        }
    }
}