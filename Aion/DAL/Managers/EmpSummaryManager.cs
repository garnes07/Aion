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

        public async Task<List<KronosEmployeeSummary>> GetActiveManagersRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.KronosEmployeeSummaries.Where(x => x.Region == crit && x.Active == true && x.ReportingRoleFlag == 1).ToListAsync();
            }
        }

        public async Task<List<KronosEmployeeSummary>> GetActiveManagersRegionUsingStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.KronosEmployeeSummaries.Where(x =>
                    x.Region == context.KronosEmployeeSummaries.FirstOrDefault(y => y.HomeBranch == crit).Region &&
                    x.Active == true && x.ReportingRoleFlag == 1).ToListAsync();
            }
        }

        public async Task<List<KronosEmployeeSummary>> GetEmployeeDetails(List<string> PersonNums)
        {
            using (var context = new WFMModel())
            {
                return await context.KronosEmployeeSummaries.Where(x => PersonNums.Any(y => y == x.PersonNumber)).ToListAsync();
            }
        }

        public async Task<KronosEmployeeSummary> GetEmployeeMatchingNumber(string personNumber)
        {
            using (var context = new WFMModel())
            {
                return await context.KronosEmployeeSummaries.Where(x => x.PersonNumber == personNumber && x.Active == true).OrderBy(x => x.PersonName).FirstOrDefaultAsync();
            }
        }

        public async Task<List<KronosEmployeeSummary>> GetEmployeeMatchingName(string PersonName)
        {
            using (var context = new WFMModel())
            {
                return await context.KronosEmployeeSummaries.Where(x => x.PersonName.Contains(PersonName) && x.Active == true).OrderBy(x => x.PersonName).ToListAsync();
            }
        }
    }
}