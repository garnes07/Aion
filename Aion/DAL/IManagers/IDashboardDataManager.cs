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
        Task<List<sp_PeriodDeploymentSummary_Result>> GetDeploymentSummaryStore(string year, byte period, string store);
        Task<List<sp_PeriodDeploymentSummary_Result>> GetDeploymentSummaryRegion(string year, byte period, string region);
        Task<List<sp_PeriodDeploymentSummary_Result>> GetDeploymentSummaryDivision(string year, byte period, string division);
        Task<List<sp_PeriodDeploymentSummary_Result>> GetDeploymentSummaryChain(string year, byte period, string chain);
        Task<List<DailyDeployment>> GetDailyDeploymentStore(string store, int weekOfYr);
        Task<List<vw_DashboardData_v2>> GetStoreDetailBetween(string store, int startWeek, int endWeek);
        Task<List<vw_DashboardData_v2>> GetRegionDetailBetween(string region, int startWeek, int endWeek);
        Task<List<vw_DashboardData_v2>> GetDivisionDetailBetween(string division, int startWeek, int endWeek);
        Task<List<vw_4WeekSummary>> Get4WeekSummaryStore(string store);
        Task<List<vw_4WeekSummary_Pilot>> Get4WeekSummaryStorePilot(string store);
        Task<List<vw_4WeekSummary>> Get4WeekSummaryRegion(string region);
        Task<List<vw_4WeekSummary_Pilot>> Get4WeekSummaryRegionPilot(string region);
        Task<List<vw_4WeekSummary>> Get4WeekSummaryDivision(string division);
        Task<List<vw_4WeekSummary>> Get4WeekSummaryChain(string chain);
        Task<List<vw_DeploymentStatus>> GetDeploymentStatusStore(string store);
        Task<List<vw_DeploymentStatus>> GetDeploymentStatusRegion(string region);
        Task<List<vw_DeploymentStatus>> GetDeploymentStatusDivision(string division);
        Task<List<vw_DeploymentStatus>> GetDeploymentStatusChain(string chain);

        Task<List<vw_DashboardData_v2_Pilot>> GetStoreDashDataPilot(string store, int weekOfYr);
        Task<vw_DailyDeployment_Pilot> GetDailyDeploymentStorePilot(string store, int weekOfYr);
        Task<List<PowerHoursProfile>> GetStorePowerHours(string store, int weekOfYr);
        Task<List<vw_DashboardData_v2_Pilot>> GetAllRegionDashDataPilot(string region, int weekOfYr);
    }
}
