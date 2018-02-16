using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class WeeksManager : IWeeksManager
    {
        public int? GetSingleWeek(DateTime dt)
        {
            using (var context = new WebMasterModel())
            {
                return context.WeekRefs.Where(x => x.Date == dt).Select(x => x.YearWeek).FirstOrDefault();
            }
        }

        public async Task<List<int?>> GetMultipleWeeks(DateTime startDate, DateTime endDate)
        {
            using (var context = new WebMasterModel())
            {
                return await context.WeekRefs.Where(x => x.Date >= startDate && x.Date <= endDate).GroupBy(x => x.YearWeek).Select(x => x.Key).OrderByDescending(x => x).ToListAsync();
            }
        }
    }
}