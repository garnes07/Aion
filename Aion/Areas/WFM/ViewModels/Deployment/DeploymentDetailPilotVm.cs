using System;
using System.Collections.Generic;
using System.Linq;
using Aion.Areas.WFM.Models.Deployment;
using Aion.ViewModels;
using Aion.Models.WFM;
using Newtonsoft.Json;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class DeploymentDetailPilotVm : BaseVm
    {
        public List<DashboardData_v2View> WeekData { get; set; }
        public DailyDeploymentView DailyData { get; set; }
        public List<PowerHoursProfileView> PowerHours { get; set; }

        private DashboardData_v2View _StoreData;
        public DashboardData_v2View StoreData => _StoreData ?? (_StoreData = WeekData.First());

        private DeploymentAggregate _RegionTotal;
        public DeploymentAggregate RegionTotal =>
            _RegionTotal ?? (_RegionTotal = WeekData.GroupBy(x => x.Region).Select(x => new DeploymentAggregate
            {
                Region = x.Key,
                SOHSpend = x.Sum(y => y.SOH),
                FinalTarget = x.Sum(y => y.FinalTarget),
                BudgetFit = x.Average(y => y.SOHUtilization),
                CustomerFit = x.Average(y => y.PayEscalations),
                DeploymentScore = x.Average(y => y.SOHUtilization + y.PayEscalations)
            }).Single());

        public string GetAdvice(decimal? toMove)
        {
            decimal _toMove = (decimal)toMove;
            decimal rounded = Math.Round(_toMove * 4) / 4;

            if (rounded == 0)
            {
                return "You've got it right here";
            }
            return rounded > 0 ? string.Format("Put up to {0} hours in a better day", rounded) : string.Format("Move {0} hours to here", rounded * -1);
        }

        public string GetName(short? a)
        {
            return Enum.GetName(typeof(DayOfWeek), a - 1).Substring(0, 3);
        }

        public string RequiredGraphArray
        {
            get
            {
                var data = new[]
                {
                    DailyData.SundayReq,
                    DailyData.MondayReq,
                    DailyData.TuesdayReq,
                    DailyData.WednesdayReq,
                    DailyData.ThursdayReq,
                    DailyData.FridayReq,
                    DailyData.SaturdayReq
                };
                return JsonConvert.SerializeObject(data);
            }
        }

        public string DeployedGraphArray
        {
            get
            {
                var data = new[]
                {
                    DailyData.SundayDeployed,
                    DailyData.MondayDeployed,
                    DailyData.TuesdayDeployed,
                    DailyData.WednesdayDeployed,
                    DailyData.ThursdayDeployed,
                    DailyData.FridayDeployed,
                    DailyData.SaturdayDeployed
                };
                return JsonConvert.SerializeObject(data);
            }
        }

        private double _totalUnders;
        public double TotalUnders
        {
            get
            {
                if (_totalUnders == -1)
                {
                    double unders = 0;

                    unders += DailyData.SundayDeployed < DailyData.SundayReq ? (double)DailyData.SundayReq - (double)DailyData.SundayDeployed : 0;
                    unders += DailyData.MondayDeployed < DailyData.MondayReq ? (double)DailyData.MondayReq - (double)DailyData.MondayDeployed : 0;
                    unders += DailyData.TuesdayDeployed < DailyData.TuesdayReq ? (double)DailyData.TuesdayReq - (double)DailyData.TuesdayDeployed : 0;
                    unders += DailyData.WednesdayDeployed < DailyData.WednesdayReq ? (double)DailyData.WednesdayReq - (double)DailyData.WednesdayDeployed : 0;
                    unders += DailyData.ThursdayDeployed < DailyData.ThursdayReq ? (double)DailyData.ThursdayReq - (double)DailyData.ThursdayDeployed : 0;
                    unders += DailyData.FridayDeployed < DailyData.FridayReq ? (double)DailyData.FridayReq - (double)DailyData.FridayDeployed : 0;
                    unders += DailyData.SaturdayDeployed < DailyData.SaturdayReq ? (double)DailyData.SaturdayReq - (double)DailyData.SaturdayDeployed : 0;

                    _totalUnders = unders;
                }
                return _totalUnders;
            }
        }

        public double TotalRequired => (double)(DailyData.SundayReq + DailyData.MondayReq + DailyData.TuesdayReq + DailyData.WednesdayReq + DailyData.ThursdayReq + DailyData.FridayReq + DailyData.SaturdayReq);

        private int _customerFitScore;
        public int CustomerFitScore
        {
            get
            {
                if (_customerFitScore != -1)
                {
                    return _customerFitScore;
                }

                var result = (int)Math.Floor(Math.Abs((TotalUnders / TotalRequired)) / (double)0.025);

                switch (result)
                {
                    case 0:
                        return _customerFitScore = 5;
                    case 1:
                        return _customerFitScore = 4;
                    case 2:
                        return _customerFitScore = 3;
                    case 3:
                        return _customerFitScore = 2;
                    case 4:
                        return _customerFitScore = 1;
                    default:
                        return _customerFitScore = 0;
                }
            }
        }

        public string GetColor(short? Score)
        {
            var rtn = "table-danger";
            if (Score >= 4)
            {
                rtn = "table-success";
            }
            else if (Score >= 3)
            {
                rtn = "table-warning";
            }
            return rtn;
        }

        public string GetColor(double? Score)
        {
            var rtn = "table-danger";
            if (Score >= 4)
            {
                rtn = "table-success";
            }
            else if (Score >= 3)
            {
                rtn = "table-warning";
            }
            return rtn;
        }

        public DeploymentDetailPilotVm()
        {
            _customerFitScore = -1;
            _totalUnders = -1;
        }
    }
}