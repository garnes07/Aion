using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class EmpSummaryManager : IEmpSummaryManager
    {
        public async Task<List<KronosEmployeeSummary>> GetActiveByRegion(string region)
        {
            using (var db = new WFMModel())
            {
                short crit = short.Parse(region);
                return await db.KronosEmployeeSummaries.Where(x => x.Region == crit && x.Active == true).ToListAsync();
            }
        }
    }
}