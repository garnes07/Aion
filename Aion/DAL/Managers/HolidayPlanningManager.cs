using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class HolidayPlanningManager : IHolidayPlanningManager
    {
        public async Task<List<vw_HolidayPlanningStore>> GetHolidayStore(string store, int startWeek, int endWeek)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.vw_HolidayPlanningStore
                    .Where(x => x.StoreNumber == crit && x.WeekNumber >= startWeek && x.WeekNumber <= endWeek)
                    .OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_HolidayPlanningStore>> GetHolidayRegion(string region, int startWeek, int endWeek)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_HolidayPlanningStore
                    .Where(x => x.Region == crit && x.StoreNumber == null && x.WeekNumber >= startWeek && x.WeekNumber <= endWeek)
                    .OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<vw_HolidayPlanningStore>> GetHolidayDivision(string division, int startWeek, int endWeek)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_HolidayPlanningStore
                    .Where(x => x.Division == division && x.Region == null && x.WeekNumber >= startWeek && x.WeekNumber <= endWeek)
                    .OrderBy(x => x.WeekNumber).ToListAsync();
            }
        }

        public async Task<List<HolidayPlanningEmp>> GetHolidayStoreEmp(string store, int startWeek)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.HolidayPlanningEmps.Where(x => x.StoreNumber == crit && x.YearStart == startWeek)
                    .OrderBy(x => x.EmployeeName).ToListAsync();
            }
        } 
        
        public async Task<List<vw_HolidayPlanningRollup>> GetHolidayRegionRollup(string region, int startWeek)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_HolidayPlanningRollup.Where(x => x.Region == crit && x.YearStart == startWeek)
                    .OrderBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<vw_HolidayPlanningRollup>> GetHolidayDivisionRollup(string division, int startWeek)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_HolidayPlanningRollup.Where(x => x.Division == division && x.StoreNumber == null && x.YearStart == startWeek)
                    .OrderBy(x => x.StoreNumber).ToListAsync();
            }
        }
    }
}