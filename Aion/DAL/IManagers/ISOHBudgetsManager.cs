using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface ISOHBudgetsManager
    {
        Task<List<vw_PublishedBudgetsUK>> GetBudgetUKStore(string store);
        Task<List<vw_PublishedBudgetsROI>> GetBudgetROIStore(string store);
        Task<List<vw_PublishedBudgetsUK>> GetBudgetUKRegion(string region);
        Task<List<vw_PublishedBudgetsROI>> GetBudgetROIRegion(string region);
    }
}