using System;
using System.Collections.Generic;
using System.Linq;
using Aion.Areas.WFM.Models.Deployment;
using Aion.DAL.Entities;
using Aion.ViewModels;
using Newtonsoft.Json;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class DeploymentDetailVm : BaseVm
    {
        public List<vw_DashboardData_v2> WeekData { get; set; }
        public List<sp_AllChainDashboardData_v2_Result> ChainData { get; set; }
        public List<DailyDeployment> DailyData { get; set; }

        public sp_AllChainDashboardData_v2_Result ChainSummary => ChainData.Single(x => x.StoreFlag != null);
        public List<sp_AllChainDashboardData_v2_Result> DivisionSummary => ChainData.Where(x => x.Division != null && x.Region == null).OrderBy(x => x.Division).ToList();
        public List<sp_AllChainDashboardData_v2_Result> RegionSummary => ChainData.Where(x => x.Region != null).OrderBy(x => x.Region).ToList();

        private vw_DashboardData_v2 _StoreData;
        public vw_DashboardData_v2 StoreData => _StoreData ?? (_StoreData = WeekData.First());

        private DailyDeployment _StoreDailyData;
        public DailyDeployment StoreDailyData => _StoreDailyData ?? (_StoreDailyData = DailyData.First());

        private DeploymentAggregate _RegionTotal;
        public DeploymentAggregate RegionTotal =>
            _RegionTotal ?? (_RegionTotal = WeekData.GroupBy(x => x.Region).Select(x => new DeploymentAggregate
            {
                Region = x.Key,
                SOHSpend = x.Sum(y => y.SOH),
                FinalTarget = x.Sum(y => y.FinalTarget),
                DeploymentScore = x.Average(y => y.SOHUtilization)
            }).Single());

        public string GetColor(short? Score)
        {
            var rtn = "table-danger";
            if (Score >= 8)
            {
                rtn = "table-success";
            }
            else if (Score >= 4)
            {
                rtn = "table-warning";
            }
            return rtn;
        }

        public string GetColor(double? Score)
        {
            var rtn = "table-danger";
            if (Score >= 8)
            {
                rtn = "table-success";
            }
            else if (Score >= 4)
            {
                rtn = "table-warning";
            }
            return rtn;
        }

        public string GetAdvice(decimal toMove)
        {
            decimal rounded = Math.Round(toMove * 4) / 4;

            if (rounded == 0)
            {
                return "You've got it right here";
            }
            return rounded > 0 ? string.Format("Put up to {0} hours in a better day", rounded) : string.Format("Move {0} hours to here", rounded * -1);
        }

        public string RequiredGraphArray
        {
            get
            {
                var data = DailyData.SelectMany(x => new[]
                {
                    Math.Round(x.SundayReq * 100, 1),
                    Math.Round(x.MondayReq * 100, 1),
                    Math.Round(x.TuesdayReq * 100, 1),
                    Math.Round(x.WednesdayReq * 100, 1),
                    Math.Round(x.ThursdayReq * 100, 1),
                    Math.Round(x.FridayReq * 100, 1),
                    Math.Round(x.SaturdayReq * 100, 1)
                });
                return JsonConvert.SerializeObject(data);
            }
        }

        public string DeployedGraphArray
        {
            get
            {
                var data = DailyData.SelectMany(x => new[]
                {
                    Math.Round(x.SundayDeployed / x.DeployedTotal * 100, 1),
                    Math.Round(x.MondayDeployed / x.DeployedTotal * 100, 1),
                    Math.Round(x.TuesdayDeployed / x.DeployedTotal * 100, 1),
                    Math.Round(x.WednesdayDeployed / x.DeployedTotal * 100, 1),
                    Math.Round(x.ThursdayDeployed / x.DeployedTotal * 100, 1),
                    Math.Round(x.FridayDeployed / x.DeployedTotal * 100, 1),
                    Math.Round(x.SaturdayDeployed / x.DeployedTotal * 100, 1)
                });
                return JsonConvert.SerializeObject(data);
            }
        }
    }
}