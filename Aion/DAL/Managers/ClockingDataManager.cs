using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class ClockingDataManager : IClockingDataManager
    {
        public async Task<List<vw_CPW_Clocking_Data>> GetClockDetailStore(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_CPW_Clocking_Data.Where(x => x.CST_CNTR_int == crit && x.FNCL_WK_NUM == weekOfYr).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Data>> GetClockDetailRegion(string region, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_CPW_Clocking_Data.Where(x => x.Region == crit && x.FNCL_WK_NUM == weekOfYr).AsNoTracking().ToListAsync();
            }
        }
        
        public async Task<List<vw_CPW_Clocking_Data>> GetClockDetailDivision(string division, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_CPW_Clocking_Data
                    .Where(x => context.vw_CPW_Clocking_Data.FirstOrDefault(y => y.Division == division).Chain == x.Chain &&
                                x.FNCL_WK_NUM == weekOfYr).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Data>> GetClockDetailChain(string chain, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_CPW_Clocking_Data.Where(x => x.Chain == chain && x.FNCL_WK_NUM == weekOfYr).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Data_Trend>> GetClockTrendStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_CPW_Clocking_Data_Trend.Where(x => x.StoreNumber == crit).OrderBy(x => x.FNCL_WK_NUM).ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Data_Trend>> GetClockTrendRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_CPW_Clocking_Data_Trend.Where(x => x.Region == crit).OrderBy(x => x.StoreNumber).ThenBy(x => x.FNCL_WK_NUM).ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Data_Trend>> GetClockTrendDivision(string division)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_CPW_Clocking_Data_Trend.Where(x => x.Division == division).OrderBy(x => x.Region).ThenBy(x => x.StoreNumber).ThenBy(x => x.FNCL_WK_NUM).ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Data_Trend>> GetClockTrendChain(string chain)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_CPW_Clocking_Data_Trend.Where(x => x.Chain == chain).OrderBy(x => x.Division)
                    .ThenBy(x => x.Region).ThenBy(x => x.StoreNumber).ThenBy(x => x.FNCL_WK_NUM).ToListAsync();
            }
        }

        public  async Task<List<vw_CPW_Clocking_Repeat_Employees>> GetRepeatOffendersStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_CPW_Clocking_Repeat_Employees.Where(x => x.StoreNumber == crit).ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Repeat_Employees>> GetRepeatOffendersRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_CPW_Clocking_Repeat_Employees.Where(x => x.Region == crit).ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Repeat_Employees>> GetRepeatOffendersDivision(string division)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_CPW_Clocking_Repeat_Employees.Where(x => x.Division == division).ToListAsync();
            }
        }

        public async Task<List<vw_CPW_Clocking_Repeat_Stores>> GetRepeatOffendersChain(string chain)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_CPW_Clocking_Repeat_Stores.Where(x => x.Chain == chain).ToListAsync();
            }
        }
    }
}