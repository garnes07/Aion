using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IEmpSummaryManager
    {
        Task<List<KronosEmployeeSummary>> GetActiveByRegion(string region);
    }
}