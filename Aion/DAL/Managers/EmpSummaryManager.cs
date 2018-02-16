using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class EmpSummaryManager : IEmpSummaryManager
    {
        public async Task<List<KronosEmployeeSummary>> GetActiveByRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.KronosEmployeeSummaries.Where(x => x.Region == crit && x.Active == true).ToListAsync();
            }
        }
    }
}