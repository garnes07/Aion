using System;

namespace Aion.Models.WFM
{
    public class DeploymentStatusView
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? WeekNumber { get; set; }
        public double Budget { get; set; }
        public double? Actual { get; set; }
        public double? Holiday { get; set; }
        public double ToBudget { get; set; }
        public double PercHoliday { get; set; }

       public string RAG 
        {
            get
            {
                var rtn = "table-success";

                if (Math.Abs(1 - (double)ToBudget) >= 0.1)
                {
                    rtn = "table-danger";
                }
                else if (Math.Abs(1 - (double)ToBudget) >= 0.05)
                {
                    rtn = "table-warning";
                }

                if (1 - ToBudget >= 0.05 && PercHoliday >= 0.1)
                {
                    rtn += " toRed";
                }

                return rtn;
            }
        }
    }
}