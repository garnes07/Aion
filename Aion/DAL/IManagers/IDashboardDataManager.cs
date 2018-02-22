using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aion.DAL.IManagers
{
    public interface IDashboardDataManager
    {
        Task<List<sp_ComplianceSummary_Result>> GetCompSummary(string year, byte period, string chain);
        Task<List<sp_ComplianceSummary_Result>> GetCompSummaryDivision(string year, byte period, string division);
        Task<List<sp_ComplianceSummaryRegion_Result>> GetCompSummaryRegion(string year, byte period, string region);
        Task<List<sp_ComplianceSummaryStore_Result>> GetCompSummaryStore(string year, byte period, string store);
        Task<List<sp_AllChainDashboardData_v2_Result>> GetAllChainDashData(string chain, int weekOfYr);
        Task<List<sp_AllChainDashboardData_v2_Result>> GetAllDivisionDashData(string division, int weekOfYr);
        Task<List<vw_DashboardData_v2>> GetAllRegionDashData(string region, int weekOfYr);
        Task<List<vw_DashboardData_v2>> GetStoreDashData(string store, int weekOfYr);
        Task<List<EmpComplianceDetail>> GetComplianceDetail(string store, int weekOfYr);
        Task<List<ShortShift>> GetShortShiftDetail(string store, int weekOfYr);
    }
}
