using System;

namespace Aion.Areas.WFM.Models.FuturePlanning
{
    public class DeploymentSwingView
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? WeekNumber { get; set; }
        public string Type { get; set; }
        public double? SOH { get; set; }
        public double? FinalTarget { get; set; }

        public double? toBudget => Math.Round((double)(SOH / FinalTarget) * 100,1);

        public int sortOrder
        {
            get
            {
                switch (Type)
                {
                    case "WeekAhead":
                        return 1;
                    case "StartOfWeek":
                        return 2;
                    case "EndOfWeek":
                        return 3;
                    case "Final":
                        return 4;
                    default:
                        return 0;
                }
            }
        }
    }
}