using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class ContractStatusManager : IContractStatusManager
    {
        public async Task<List<vw_StoreContractStatus>> GetContractStatusStore(string storeNumber)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(storeNumber);
                return await context.vw_StoreContractStatus
                    .Where(x => x.Region == context.vw_StoreContractStatus.FirstOrDefault(y => y.StoreNumber == crit).Region && x.StoreNumber != null)
                    .OrderBy(x => x.StoreNumber)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_StoreContractStatus>> GetContractStatusRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_StoreContractStatus
                    .Where(x => x.Region == crit && x.StoreNumber != null)
                    .OrderBy(x => x.Region)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_StoreContractStatus>> GetContractStatusDivision(string division)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_StoreContractStatus
                    .Where(x => x.Chain == context.vw_StoreContractStatus.FirstOrDefault(y => y.Division == division).Chain && x.StoreNumber == null)
                    .OrderBy(x => x.Region)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_StoreContractStatus>> GetContractStatusChain(string chain)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_StoreContractStatus
                    .Where(x => x.Chain == chain && x.StoreNumber == null)
                    .OrderBy(x => x.Region)
                    .ToListAsync();
            }
        }
    }
}