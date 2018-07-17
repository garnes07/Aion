using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IEmpSummaryManager
    {
        Task<List<KronosEmployeeSummary>> GetActiveByRegion(string region);
        Task<List<KronosEmployeeSummary>> GetAllByRegion(string region);
        Task<List<KronosEmployeeSummary>> GetActiveManagersRegion(string region);
        Task<List<KronosEmployeeSummary>> GetActiveManagersRegionUsingStore(string store);
        Task<List<KronosEmployeeSummary>> GetEmployeeDetails(List<string> PersonNums);
        Task<KronosEmployeeSummary> GetEmployeeMatchingNumber(string personNumber);
        Task<List<KronosEmployeeSummary>> GetEmployeeMatchingName(string PersonName);
        vw_ROIMismatch CheckROIRemap(string payrollNum);
    }
}