using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class PayCalendarManager : IPayCalendarManager
    {
        public async Task<List<PayCalendarRef>> GetPayCalendarRef(string chain)
        {
            using(var context = new WFMModel())
            {
                return await context.PayCalendarRefs.Where(x => x.Chain == chain).OrderBy(x => x.PayDate).ToListAsync();
            }
        }

        public async Task<List<PayCalendarDate>> GetPayCalendarDates(string chain, string period)
        {
            using(var context = new WFMModel())
            {
                string[] crit = period.Split('_');
                string zYear = crit[0];
                string zPeriod = crit[1];

                return await context.PayCalendarDates.Where(x => x.Chain == chain && x.Period == zPeriod && x.Year == zYear).OrderBy(x => x.WCDate).ToListAsync();
            }
        }
    }
}