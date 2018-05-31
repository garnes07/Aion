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

        public async Task<List<WFM_EMPLOYEE_INFO>> GetStaffListStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.WFM_EMPLOYEE_INFO.Where(x => x.CostCentre == crit)
                    .OrderByDescending(x => x.Grade).ThenBy(x => x.JobCode).ThenByDescending(x => x.Std_Hrs_Wk)
                    .ToListAsync();
            }
        }
    }
}