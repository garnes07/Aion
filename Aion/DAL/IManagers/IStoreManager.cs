using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aion.DAL.IManagers
{
    public interface IStoreManager
    {
        Task<List<StoreMaster>> GetStoreDetails(string ip);
        Task<List<StoreMaster>> GetStoresInSameRegion(string storeNumber);
        Task<string> GetKronosName(string storeNum);
        Task<List<StoreMaster>> GetStoreMenu(short[] _storeNumber);
        Task<List<StoreMaster>> GetStoreMenu(short[] _storeNumber, string ip);
        Task<List<StoreMaster>> GetRegionMenu(short[] _region);
        Task<List<StoreMaster>> GetDivisionMenu(string[] _division);
        Task<List<StoreMaster>> GetChainMenu(string[] _chain);
        Task<List<StoreMaster>> GetAllMenu();
        Task<List<vw_StoreLocations>> GetAllStoreLocations();
        Task<List<StoreMaster>> GetStoresInRegion(string region);
    }
}
