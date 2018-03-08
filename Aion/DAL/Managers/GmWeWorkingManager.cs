using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class GmWeWorkingManager : IGmWeWorkingManager
    {
        public async Task<List<vw_GmWeWorking>> GetGmWorkingRegionUsingStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_GmWeWorking.Where(
                    x => x.Region == context.vw_GmWeWorking.FirstOrDefault(y => y.StoreNumber == crit).Region
                         ).ToListAsync();
            }
        }

        public async Task<List<vw_GmWeWorking>> GetGmWorkingRegion(string regionNo)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(regionNo);
                return await context.vw_GmWeWorking.Where(x => x.Region == crit).ToListAsync();
            }
        }

        public async Task<List<vw_GmWeWorking>> GetGmWorkingDivision(string division)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_GmWeWorking.Where(x => 
                    (context.vw_GmWeWorking.FirstOrDefault(y => y.Division == division).Chain == x.Chain && x.Division == null) || 
                    (x.Division == division && x.StoreNumber == null)).ToListAsync();
            }
        }

        public async Task<List<vw_GmWeWorking>> GetGmWorkingChain(string chain)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_GmWeWorking.Where(x => x.Chain == chain && x.StoreNumber == null).ToListAsync();
            }
        }
    }
}