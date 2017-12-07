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
            using (var dbContext = new WebMasterModel())
            {
                return await dbContext.StoreMasters.Where(x => x.IpRange == ip).Include(x => x.IpRefs).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetStoreMenu(short _storeNumber)
        {
            using (var dbContext = new WebMasterModel())
            {
                return await dbContext.StoreMasters.Where(x => x.StoreNumber == _storeNumber).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetRegionMenu(short[] _region)
        {
            using (var dbContext = new WebMasterModel())
            {
                return await dbContext.StoreMasters.Where(x => _region.Contains(x.Region)).OrderBy(x => x.StoreNumber).ToListAsync();
            }
        }
        
        public async Task<List<StoreMaster>> GetDivisionMenu(string _division)
        {
            using (var dbContext = new WebMasterModel())
            {
                return await dbContext.StoreMasters.Where(x => x.Division == _division).OrderBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetChainMenu(string _chain)
        {
            using (var dbContext = new WebMasterModel())
            {
                return await dbContext.StoreMasters.Where(x => x.Chain == _chain).OrderBy(x => x.Chain).ThenBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetAllMenu()
        {
            using (var dbContext = new WebMasterModel())
            {
                return await dbContext.StoreMasters.OrderBy(x => x.Chain).ThenBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }
    }
}