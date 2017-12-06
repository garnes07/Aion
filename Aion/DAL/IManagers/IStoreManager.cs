using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aion.DAL.IManagers
{
    public interface IStoreManager
    {
        Task<List<StoreMaster>> GetStoreDetails(string ip);
        Task<List<StoreMaster>> GetStoreMenu(short _storeNumber);
        Task<List<StoreMaster>> GetRegionMenu(short _region);
        Task<List<StoreMaster>> GetDivisionMenu(string _chain);
    }
}
