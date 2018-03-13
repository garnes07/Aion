using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class ScheduleManager : IScheduleManager
    {
        public async Task<List<vw_ScheduleData>> GetStoreSchedule(string store, int weekNumber)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_ScheduleData.Where(x =>
                    x.WeekNumber == weekNumber && (x.StoreNumber == crit || x.TransferBranch == store))
                    .OrderBy(x => x.JobCode).ThenBy(x => x.FullName).ThenBy(x => x.DayNum)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_ScheduleData>> GetRegionGMScheduleData(string region, int weekNumber)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_ScheduleData.Where(x =>
                        x.WeekNumber == weekNumber && x.Region == crit && x.JobCode == 1)
                    .OrderBy(x => x.StoreNumber).ThenBy(x => x.FullName).ThenBy(x => x.DayNum)
                    .ToListAsync();
            }
        }
    }
}