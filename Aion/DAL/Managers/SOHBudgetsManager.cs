using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class SOHBudgetsManager : ISOHBudgetsManager
    {
        public async Task<List<vw_PublishedBudgetsUK>> GetBudgetUKStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_PublishedBudgetsUK.Where(x => x.StoreNumber == crit).OrderBy(x => x.YearWeek)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_PublishedBudgetsROI>> GetBudgetROIStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_PublishedBudgetsROI.Where(x => x.StoreNumber == crit).OrderBy(x => x.YearWeek)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_PublishedBudgetsUK>> GetBudgetUKRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_PublishedBudgetsUK.Where(x => x.Region == crit).OrderBy(x => x.YearWeek)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_PublishedBudgetsROI>> GetBudgetROIRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_PublishedBudgetsROI.Where(x => x.Region == crit).OrderBy(x => x.YearWeek)
                    .ToListAsync();
            }
        }
    }
}