using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aion.DAL.IManagers
{
    public interface IStoreManager
    {
        Task<Store> GetStoreDetails(string ip);
        Task<List<sp_GetBranchMenu_Result>> GetBranchMenu(int _lvl, string _area);
    }
}
