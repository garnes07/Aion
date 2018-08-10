using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using System;
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

        public async Task<List<StoreMaster>> GetStoresInSameRegion(string storeNumber)
        {
            using(var context = new WebMasterModel())
            {
                short crit = short.Parse(storeNumber);
                return await context.StoreMasters.Where(x => x.Region == context.StoreMasters.FirstOrDefault(y => y.StoreNumber == crit).Region).ToListAsync();
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

        public async Task<List<StoreMaster>> GetStoreMenu(short[] _storeNumber)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => _storeNumber.Contains(x.StoreNumber)).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetStoreMenu(short[] _storeNumber, string ip)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => _storeNumber.Contains(x.StoreNumber) || x.IpRange == ip).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetRegionMenu(short[] _region)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => _region.Contains(x.Region)).OrderBy(x => x.StoreNumber).ToListAsync();
            }
        }
        
        public async Task<List<StoreMaster>> GetDivisionMenu(string[] _division)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => _division.Contains(x.Division)).OrderBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetChainMenu(string[] _chain)
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.Where(x => _chain.Contains(x.Chain)).OrderBy(x => x.Chain).ThenBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetAllMenu()
        {
            using (var context = new WebMasterModel())
            {
                return await context.StoreMasters.OrderBy(x => x.Chain).ThenBy(x => x.Region).ThenBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<vw_StoreLocations>> GetAllStoreLocations()
        {
            using (var context = new WFMModel())
            {
                return await context.vw_StoreLocations.OrderBy(x => x.Region).ToListAsync();
            }
        }

        public async Task<List<StoreMaster>> GetStoresInRegion(string region)
        {
            using(var context = new WebMasterModel())
            {
                short crit = short.Parse(region);
                return await context.StoreMasters.Where(x => x.Region == crit).ToListAsync();
            }
        }

        public async Task<bool> LogStoreSession(short storeNum)
        {
            using(var context = new WebMasterModel())
            {
                try
                {
                    context.StoreSessionLogs.Add(new StoreSessionLog
                    {
                        StoreNumber = storeNum,
                        Timestamp = DateTime.Now
                    });

                    await context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}