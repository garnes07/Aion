using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class ClockingDataManager : IClockingDataManager
    {
        public async Task<List<CPW_Clocking_Data>> GetClockDetailStore(string store, int weekOfYr)
        {
            using (var dbContext = new WFMModel())
            {
                short crit = short.Parse(store);
                return await dbContext.CPW_Clocking_Data.Where(x => x.CST_CNTR_int == crit && x.FNCL_WK_NUM == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Data>> GetClockDetailRegion(string region, int weekOfYr)
        {
            using (var dbContext = new WFMModel())
            {
                short crit = short.Parse(region);
                return await dbContext.vw_CPW_Clocking_Data.Where(x => x.Region == crit && x.FNCL_WK_NUM == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Data>> GetClockDetailDivision(string division, int weekOfYr)
        {
            using (var dbContext = new WFMModel())
            {
                return await dbContext.vw_CPW_Clocking_Data.Where(x => x.Division == division && x.FNCL_WK_NUM == weekOfYr).ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Data>> GetClockDetailChain(string chain, int weekOfYr)
        {
            using (var dbContext = new WFMModel())
            {
                return await dbContext.vw_CPW_Clocking_Data.Where(x => x.Chain == chain && x.FNCL_WK_NUM == weekOfYr).ToListAsync();
            }
        }
    }
}