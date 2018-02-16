using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class DashboardDataManager : IDashboardDataManager
    {
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

        public async Task<List<DashBoardData_v2>> GetAllRegionDashData(string region, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.DashBoardData_v2.Where(x => x.Region == crit && x.WeekNumber == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<DashBoardData_v2>> GetStoreDashData(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.DashBoardData_v2.Where(x => x.StoreNumber == crit && x.WeekNumber == weekOfYr).ToListAsync();
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

        public async Task<List<ShortShift>> GetShortShiftDetail(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.ShortShifts.Where(x => x.WeekNumber == weekOfYr && x.HomeStore == crit).ToListAsync();
            }
        }
    }
}