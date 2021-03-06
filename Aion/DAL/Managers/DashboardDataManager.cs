﻿using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class DashboardDataManager : IDashboardDataManager
    {
        public async Task<bool> CheckTop100(string store)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(store);
                var result = await context.Top100Ref.Where(x => x.StoreNumber == crit).FirstOrDefaultAsync();
                return result != null;
            }
        }

        public async Task<List<sp_ComplianceSummary_Result>> GetCompSummary(string year, byte period, string chain)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_ComplianceSummary(chain, period, year).ToList());
            }
        }

        public async Task<List<sp_ComplianceSummary_Result>> GetCompSummaryDivision(string year, byte period, string division)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_ComplianceSummaryDivision(division, period, year).ToList());
            }
        }

        public async Task<List<sp_ComplianceSummaryRegion_Result>> GetCompSummaryRegion(string year, byte period, string region)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_ComplianceSummaryRegion(region, period, year).ToList());
            }
        }

        public async Task<List<sp_ComplianceSummaryStore_Result>> GetCompSummaryStore(string year, byte period, string store)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_ComplianceSummaryStore(store, period, year).ToList());
            }
        }

        public async Task<List<sp_AllChainDashboardData_v2_Result>> GetAllChainDashData(string chain, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_AllChainDashboardData_v2(chain, weekOfYr).ToList());
            }
        }

        public async Task<List<sp_AllChainDashboardData_v2_Result>> GetAllDivisionDashData(string division, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_AllDivisionDashboardData_v2(division, weekOfYr).ToList());
            }
        }

        public async Task<List<vw_DashboardData_v2>> GetAllRegionDashData(string region, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_DashboardData_v2.Where(x => x.Region == crit && x.WeekNumber == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<vw_DashboardData_v2>> GetStoreDashData(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_DashboardData_v2.Where(x => x.StoreNumber == crit && x.WeekNumber == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<EmpComplianceDetail>> GetComplianceDetail(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.EmpComplianceDetails.Where(x => x.WeekNumber == weekOfYr && x.StoreNumber == crit).ToListAsync();
            }
        }

        public async Task<List<sp_PeriodDeploymentSummary_Result>> GetDeploymentSummaryStore(string year, byte period, string store)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_PeriodDeploymentSummary(1, store, year, period).ToList());
            }
        }

        public async Task<List<sp_PeriodDeploymentSummary_Result>> GetDeploymentSummaryRegion(string year, byte period, string region)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_PeriodDeploymentSummary(2, region, year, period).ToList());
            }
        }

        public async Task<List<sp_PeriodDeploymentSummary_Result>> GetDeploymentSummaryDivision(string year, byte period, string division)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_PeriodDeploymentSummary(3, division, year, period).ToList());
            }
        }

        public async Task<List<sp_PeriodDeploymentSummary_Result>> GetDeploymentSummaryChain(string year, byte period, string chain)
        {
            using (var context = new WFMModel())
            {
                return await Task.Run(() => context.sp_PeriodDeploymentSummary(4, chain, year, period).ToList());
            }
        }

        public async Task<List<DailyDeployment>> GetDailyDeploymentStore(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.DailyDeployments.Where(x => x.StoreNumber == crit && x.WeekNumber == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<vw_DashboardData_v2>> GetStoreDetailBetween(string store, int startWeek, int endWeek)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_DashboardData_v2
                    .Where(x => x.StoreNumber == crit && x.WeekNumber >= startWeek && x.WeekNumber <= endWeek)
                    .OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_DashboardData_v2>> GetRegionDetailBetween(string region, int startWeek, int endWeek)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_DashboardData_v2
                    .Where(x => x.Region == crit && x.WeekNumber >= startWeek && x.WeekNumber <= endWeek)
                    .OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_DashboardData_v2>> GetDivisionDetailBetween(string division, int startWeek, int endWeek)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_DashboardData_v2
                    .Where(x => x.Division == division && x.WeekNumber >= startWeek && x.WeekNumber <= endWeek)
                    .OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_DashboardData_v2>> GetChainDetailBetween(string chain, int startWeek, int endWeek)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_DashboardData_v2
                    .Where(x => x.StoreFlag == chain && x.WeekNumber >= startWeek && x.WeekNumber <= endWeek)
                    .OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_4WeekSummary>> Get4WeekSummaryStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_4WeekSummary.Where(x => x.StoreNumber == crit).OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_4WeekSummary_Pilot>> Get4WeekSummaryStorePilot(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_4WeekSummary_Pilot.Where(x => x.StoreNumber == crit).OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_4WeekSummary>> Get4WeekSummaryRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_4WeekSummary.Where(x => x.Region == crit && x.StoreNumber == null).OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_4WeekSummary_Pilot>> Get4WeekSummaryRegionPilot(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_4WeekSummary_Pilot.Where(x => x.Region == crit && x.StoreNumber == null).OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_4WeekSummary>> Get4WeekSummaryDivision(string division)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_4WeekSummary.Where(x => x.Division == division && x.Region == null).OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_4WeekSummary>> Get4WeekSummaryChain(string chain)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_4WeekSummary.Where(x => x.Chain == chain && x.Division == null).OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_DeploymentStatus>> GetDeploymentStatusStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_DeploymentStatus
                    .Where(x => x.Region == context.vw_DashboardData_v2.FirstOrDefault(y => y.StoreNumber == crit).Region)
                    .OrderBy(x => x.StoreNumber)
                    .ThenBy(x => x.WeekNumber)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_DeploymentStatus>> GetDeploymentStatusRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_DeploymentStatus
                    .Where(x => x.Region == crit)
                    .OrderBy(x => x.StoreNumber)
                    .ThenBy(x => x.WeekNumber)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_DeploymentStatus>> GetDeploymentStatusDivision(string division)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_DeploymentStatus
                    .Where(x => x.Chain == context.vw_DashboardData_v2.FirstOrDefault(y => y.Division == division).StoreFlag 
                        && (x.Division == null || x.Division == division)
                        && x.StoreNumber == null)
                    .OrderBy(x => x.Division)
                    .ThenBy(x => x.Region)
                    .ThenBy(x => x.WeekNumber)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_DeploymentStatus>> GetDeploymentStatusChain(string chain)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_DeploymentStatus
                    .Where(x => x.Chain == chain && x.StoreNumber == null)
                    .OrderBy(x => x.Division)
                    .ThenBy(x => x.Region)
                    .ThenBy(x => x.WeekNumber)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_DashboardData_v2_Pilot>> GetStoreDashDataPilot(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_DashboardData_v2_Pilot.Where(x => x.StoreNumber == crit && x.WeekNumber == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<vw_DashboardData_v2_Top100>> GetStoreDashDataTop100(string store, int weekOfYr)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_DashboardData_v2_Top100.Where(x => x.StoreNumber == crit && x.WeekNumber == weekOfYr).ToListAsync();
            }
        }

        public async Task<vw_DailyDeployment_Pilot> GetDailyDeploymentStorePilot(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_DailyDeployment_Pilot.Where(x => x.StoreNumber == crit && x.WeekNumber == weekOfYr).FirstOrDefaultAsync();
            }
        }

        public async Task<vw_DailyDeployment_Top100> GetDailyDeploymentStoreTop100(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_DailyDeployment_Top100.Where(x => x.StoreNumber == crit && x.WeekNumber == weekOfYr).FirstOrDefaultAsync();
            }
        }

        public async Task<List<PowerHoursProfile>> GetStorePowerHours(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.PowerHoursProfiles.Where(x => x.Store == crit && x.Week == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<vw_DashboardData_v2_Pilot>> GetAllRegionDashDataPilot(string region, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_DashboardData_v2_Pilot.Where(x => x.Region == crit && x.WeekNumber == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<vw_SOHSwings>> GetSOHSwingsChain(string chain)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_SOHSwings.Where(x => x.Chain == chain && x.StoreNumber == null)
                    .OrderBy(x => x.Division)
                    .ThenBy(x => x.Region)
                    .ThenBy(x => x.WeekNumber)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_SOHSwings>> GetSOHSwingsDivision(string division)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_SOHSwings.Where(x => x.Chain == context.vw_DashboardData_v2.FirstOrDefault(y => y.Division == division).StoreFlag
                        && (x.Division == null || x.Division == division)
                        && x.StoreNumber == null)
                    .OrderBy(x => x.Division)
                    .ThenBy(x => x.Region)
                    .ThenBy(x => x.WeekNumber)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_SOHSwings>> GetSOHSwingsStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_SOHSwings
                    .Where(x => x.Region == context.vw_DashboardData_v2.FirstOrDefault(y => y.StoreNumber == crit).Region)
                    .OrderBy(x => x.StoreNumber)
                    .ThenBy(x => x.WeekNumber)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_SOHSwings>> GetSOHSwingsRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_SOHSwings
                    .Where(x => x.Region == crit)
                    .OrderBy(x => x.StoreNumber)
                    .ThenBy(x => x.WeekNumber)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_Top100CreditSummary>> GetCreditSummaryWeek(int weekOfYr)
        {
            using(var context = new WFMModel())
            {
                return await context.vw_Top100CreditSummary.Where(x => x.WeekNumber == weekOfYr).OrderBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<vw_StoreDeploymentDash>> GetStoreDeploymentDashByStore(string year, byte period, string store)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(store);
                string _year = year;
                byte _period = period;

                if(year == "e")
                {
                    using(var weekContext = new WebMasterModel())
                    {
                        var today = DateTime.Now.ToShortDateString();
                        var result = await weekContext.WeekRefs.FirstOrDefaultAsync(x => x.DateString == today);
                        _year = result.FY;
                        _period = (byte)result.Period;
                    }
                }

                var region = (await context.vw_DashboardData_v2.FirstOrDefaultAsync(y => y.StoreNumber == crit)).Region;

                return await context.vw_StoreDeploymentDash
                .Where(x => x.Region == region && x.Year == _year && x.Period == _period)
                .OrderBy(x => x.StoreNumber)
                .ThenBy(x => x.WeekNumber)
                .ToListAsync();            
            }
        }

        public async Task<List<vw_StoreDeploymentDash>> GetStoreDeploymentDashByRegion(string year, byte period, string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                string _year = year;
                byte _period = period;

                if (year == "e")
                {
                    using (var weekContext = new WebMasterModel())
                    {
                        var today = DateTime.Now.ToShortDateString();
                        var result = await weekContext.WeekRefs.FirstOrDefaultAsync(x => x.DateString == today);
                        _year = result.FY;
                        _period = (byte)result.Period;
                    }
                }

                return await context.vw_StoreDeploymentDash
                .Where(x => x.Region == crit && x.Year == _year && x.Period == _period)
                .OrderBy(x => x.StoreNumber)
                .ThenBy(x => x.WeekNumber)
                .ToListAsync();
            }
        }

        public async Task<List<vw_StoreDeploymentRank>> GetStoreDeploymentRankByStore(string year, byte period, string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                string _year = year;
                byte _period = period;

                if (year == "e")
                {
                    using (var weekContext = new WebMasterModel())
                    {
                        var today = DateTime.Now.ToShortDateString();
                        var result = await weekContext.WeekRefs.FirstOrDefaultAsync(x => x.DateString == today);
                        _year = result.FY;
                        _period = (byte)result.Period;
                    }
                }                

                return await context.vw_StoreDeploymentRank
                .Where(x => x.Region == context.vw_DashboardData_v2.FirstOrDefault(y => y.StoreNumber == crit).Region && x.Year == _year && x.Period == _period)
                .OrderBy(x => x.StoreNumber)
                .ThenBy(x => x.WeekNumber)
                .ToListAsync();
            }
        }

        public async Task<List<vw_StoreDeploymentRank>> GetStoreDeploymentRankByRegion(string year, byte period, string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                string _year = year;
                byte _period = period;

                if (year == "e")
                {
                    using (var weekContext = new WebMasterModel())
                    {
                        var today = DateTime.Now.ToShortDateString();
                        var result = await weekContext.WeekRefs.FirstOrDefaultAsync(x => x.DateString == today);
                        _year = result.FY;
                        _period = (byte)result.Period;
                    }
                }

                return await context.vw_StoreDeploymentRank
                .Where(x => x.Region == crit && x.Year == _year && x.Period == _period)
                .OrderBy(x => x.StoreNumber)
                .ThenBy(x => x.WeekNumber)
                .ToListAsync();
            }
        }

        public async Task<List<vw_StoreDeploymentDashTrend>> GetStoreDeploymentDashTrendByStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);

                var region = (await context.vw_DashboardData_v2.FirstOrDefaultAsync(y => y.StoreNumber == crit)).Region;

                return await context.vw_StoreDeploymentDashTrend
                .Where(x => x.Region == region)
                .OrderBy(x => x.StoreNumber)
                .ToListAsync();
            }
        }

        public async Task<List<vw_StoreDeploymentDashTrend>> GetStoreDeploymentDashTrendByRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);

                return await context.vw_StoreDeploymentDashTrend
                .Where(x => x.Region == crit)
                .OrderBy(x => x.StoreNumber)
                .ToListAsync();
            }
        }

        public async Task<List<vw_StoreDeploymentRankTrend>> GetStoreDeploymentRankTrendByStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);

                var region = (await context.vw_DashboardData_v2.FirstOrDefaultAsync(y => y.StoreNumber == crit)).Region;

                return await context.vw_StoreDeploymentRankTrend
                .Where(x => x.Region == region)
                .OrderBy(x => x.StoreNumber)
                .ToListAsync();
            }
        }

        public async Task<List<vw_StoreDeploymentRankTrend>> GetStoreDeploymentRankTrendByRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);

                return await context.vw_StoreDeploymentRankTrend
                .Where(x => x.Region == crit)
                .OrderBy(x => x.StoreNumber)
                .ToListAsync();
            }
        }

        public async Task<List<PeakData>> GetStorePeakData(string store)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.PeakDatas.Where(x => x.StoreNumber == crit).OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }
    }
}