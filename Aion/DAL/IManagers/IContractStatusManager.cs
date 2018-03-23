using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IContractStatusManager
    {
        Task<List<vw_StoreContractStatus>> GetContractStatusChain(string chain);
        Task<List<vw_StoreContractStatus>> GetContractStatusDivision(string division);
        Task<List<vw_StoreContractStatus>> GetContractStatusRegion(string region);
        Task<List<vw_StoreContractStatus>> GetContractStatusStore(string storeNumber);
    }
}