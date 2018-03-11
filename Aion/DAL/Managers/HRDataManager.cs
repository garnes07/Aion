using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class HRDataManager : IHRDataManager
    {
        public async Task<vw_ContractBaseDetail> GetContractAndHolidayStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_ContractBaseDetail.Where(x => x.StoreNumber == crit).FirstOrDefaultAsync();
            }
        }

        public async Task<List<vw_ContractBaseDetail>> GetContractAndHolidayRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_ContractBaseDetail.Where(x => x.Region == crit).ToListAsync();
            }
        }

        public async Task<List<HrFeed>> GetStaffListStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.HrFeeds.Where(x => x.STORE_NUM == crit && x.DOL == "")
                    .OrderByDescending(x => x.AnnualBasic).ThenByDescending(x => x.CONTRACT_HOURS)
                    .ThenBy(x => x.SURNAME).ToListAsync();
            }
        }
    }
}