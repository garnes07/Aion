using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class StoreManager : IStoreManager
    {
        public async Task<Store> GetStoreDetails(string ip)
        {
            var ipBase = ip == "::1" ? "10.224.240" : ip.Substring(0, ip.LastIndexOf("."));

            using (var dbContext = new WfmContext())
            {
                var result = await dbContext.Stores.Where(x => x.IpRange == ipBase).ToListAsync();

                if (result.Count() > 1)
                {
                    return new Store { IpRange = "DUPLICATE" };
                }
                else
                {
                    return result.FirstOrDefault();
                }
            }
        }

        public async Task<List<sp_GetBranchMenu_Result>> GetBranchMenu (int _lvl, string _area)
        {
            using (var dbContext = new WfmContext())
            {
                return await Task.Run(() => dbContext.sp_GetBranchMenu((byte)_lvl, _area).ToList());
            }
        }

    }
}