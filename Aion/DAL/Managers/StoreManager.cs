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
        public async Task<List<StoreMaster>> GetStoreDetails(string ip)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => x.IpRange == ip).Include(x => x.IpRefs).ToListAsync();
            }
        }

        public async Task<string> GetKronosName(string storeNum)
        {
            using (var context = new WebMasterModel())
            {
                short crit = short.Parse(storeNum);
                var result = await context.StoreMasters.Where(x => x.StoreNumber == crit).FirstOrDefaultAsync();
                return result.KronosName;
            }
        }

        public async Task<List<StoreMaster>> GetStoreMenu(short _storeNumber)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => x.StoreNumber == _storeNumber).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetRegionMenu(short[] _region)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => _region.Contains(x.Region)).OrderBy(x => x.StoreNumber).ToListAsync();
            }
        }
        
        public async Task<List<StoreMaster>> GetDivisionMenu(string _division)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => x.Division == _division).OrderBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetChainMenu(string _chain)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => x.Chain == _chain).OrderBy(x => x.Chain).ThenBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetAllMenu()
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.OrderBy(x => x.Chain).ThenBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }
    }
}