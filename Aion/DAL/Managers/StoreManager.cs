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

        public async Task<List<StoreMaster>> GetRegionMenu(short _region)
        {
            using (var dbContext = new WebMasterModel())
            {
                return await dbContext.StoreMasters.Where(x => x.Region == _region).ToListAsync();
            }
        }
        
        public async Task<List<StoreMaster>> GetDivisionMenu(string _chain)
        {
            using (var dbContext = new WebMasterModel())
            {
                return await dbContext.StoreMasters.Where(x => x.Chain == _chain).ToListAsync();
            }
        }
    }
}