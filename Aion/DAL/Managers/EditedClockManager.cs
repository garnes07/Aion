using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class EditedClockManager : IEditedClockManager
    {
        public async Task<List<EditedClock>> GetEditedClocksStore(string store, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.EditedClocks.Where(x => x.WeekNumber == weekOfYr && x.HomeStore == crit).ToListAsync();
            }
        }

        public async Task<List<vw_EditedClocks>> GetEditedClocksRegion(string region, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_EditedClocks.Where(x => x.Region == crit && x.WeekNumber == weekOfYr).OrderBy(x => x.StoreNumber).ThenBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_EditedClocks>> GetEditedClocksDivision(string division, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_EditedClocks
                    .Where(x => x.Chain == context.vw_EditedClocks.FirstOrDefault(y => y.Division == division).Chain &&
                                x.WeekNumber == weekOfYr).OrderBy(x => x.Region).ThenBy(x => x.StoreNumber)
                    .ThenBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_EditedClocks>> GetEditedClocksChain(string chain, int weekOfYr)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_EditedClocks.Where(x => x.Chain == chain && x.WeekNumber == weekOfYr).OrderBy(x => x.Division).ThenBy(x => x.Region).ThenBy(x => x.StoreNumber).ThenBy(x => x.WeekNumber).ToListAsync();
            }
        }
    }
}