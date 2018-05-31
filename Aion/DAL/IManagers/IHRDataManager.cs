using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IHRDataManager
    {
        Task<vw_ContractBaseDetail> GetContractAndHolidayStore(string store);
        Task<List<vw_ContractBaseDetail>> GetContractAndHolidayRegion(string region);
        Task<List<WFM_EMPLOYEE_INFO>> GetStaffListStore(string store);
    }
}