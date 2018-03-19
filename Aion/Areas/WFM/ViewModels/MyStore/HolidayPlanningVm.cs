using System;
using System.Collections.Generic;
using System.Linq;
using Aion.DAL.Entities;
using Aion.ViewModels;
using Newtonsoft.Json;

namespace Aion.Areas.WFM.ViewModels.MyStore
{
    public class HolidayPlanningVm : BaseVm
    {
        public List<vw_HolidayPlanningStore> StoreCollection { get; set; }
        public List<HolidayPlanningEmp> EmpCollection { get; set; }
        public List<vw_HolidayPlanningRollup> RollupCollection { get; set; }
        public List<vw_DashboardData_v2> DashCollection { get; set; }
        public int CurrentWeek { get; set; }
        
        public int? YTDTaken => StoreCollection.Sum(x => x.Taken);
        public int? YTDGuideline => StoreCollection.Where(x => x.WeekNumber < CurrentWeek).Sum(x => x.Guideline);
        public float PercTaken => (float)YTDTaken / (float)YTDGuideline;

        private float? _PercToTake;
        public float? PercToTake => _PercToTake ?? (_PercToTake = YTDGuideline / (float)StoreCollection.Sum(x => x.Guideline));

        public string DeployedGraphArray
        {
            get
            {
                return JsonConvert.SerializeObject(DashCollection.Where(x => x.FinalTarget != null)
                    .GroupBy(x => x.WeekNumber)
                    .Select(x => Math.Round((double) (x.Sum(y => y.SOH) / x.Sum(y => y.FinalTarget) * 100), 0)));
            }
        }

        public string GuideGraphArray
        {
            get
            {
                return JsonConvert.SerializeObject(StoreCollection.Select(x => x.Guideline ?? 0));
            }
        }

        public string TakenGraphArray
        {
            get
            {
                return JsonConvert.SerializeObject(StoreCollection.Select(x => x.Taken ?? 0));
            }
        }

        public string ScheduledGraphArray
        {
            get
            {
                return JsonConvert.SerializeObject(StoreCollection.Select(x => x.Scheduled ?? 0));
            }
        }

        public char RAG(HolidayPlanningEmp entry)
        {
            float? takenPerc = (float)entry.Taken / (entry.Balance + entry.Taken);
            float? RAGdelta = Math.Abs((float)takenPerc - (float)PercToTake);

            if (RAGdelta >= 0.2)
            {
                return 'R';
            }
            else if (RAGdelta >= 0.1)
            {
                return 'A';
            }
            else
            {
                return 'G';
            }
        }

        public char RAG(vw_HolidayPlanningRollup entry)
        {
            float? takenPerc = (float)entry.Taken / (entry.Balance + entry.Taken);
            float? RAGdelta = Math.Abs((float)takenPerc - (float)PercToTake);

            if (RAGdelta >= 0.2)
            {
                return 'R';
            }
            else if (RAGdelta >= 0.1)
            {
                return 'A';
            }
            else
            {
                return 'G';
            }
        }
    }
}