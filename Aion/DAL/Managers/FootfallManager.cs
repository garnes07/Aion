using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class FootfallManager : IFootfallManager
    {
        public async Task<List<vw_FootfallHourly>> GetFootfallStore(string store, string year, int weeknumber)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_FootfallHourly
                    .Where(x => x.StoreNumber == crit && x.Year == year && x.WeekNumber == weeknumber)
                    .OrderBy(x => x.Day).ThenBy(x => x.Hour_In_Day_24).ToListAsync();
            }
        }

        public async Task<List<vw_FootfallHourly>> GetFootfallRegion(string region, string year, int weeknumber)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_FootfallHourly
                    .Where(x => x.Region == crit && x.Year == year && x.WeekNumber == weeknumber)
                    .OrderBy(x => x.Day).ThenBy(x => x.Hour_In_Day_24).ToListAsync();
            }
        }

        public async Task<List<vw_FootfallHourly>> GetFootfallDivision(string division, string year, int weeknumber)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_FootfallHourly
                    .Where(x => x.Division == division && x.Year == year && x.WeekNumber == weeknumber)
                    .OrderBy(x => x.Day).ThenBy(x => x.Hour_In_Day_24).ToListAsync();
            }
        }

        public async Task<List<vw_FootfallHourly>> GetFootfallChain(string chain, string year, int weeknumber)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_FootfallHourly
                    .Where(x => x.Chain == chain && x.Year == year && x.WeekNumber == weeknumber)
                    .OrderBy(x => x.Day).ThenBy(x => x.Hour_In_Day_24).ToListAsync();
            }
        }
    }
}