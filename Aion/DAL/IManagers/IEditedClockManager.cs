using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IEditedClockManager
    {
        Task<List<EditedClock>> GetEditedClocksStore(string store, int weekOfYr);
        Task<List<vw_EditedClocks>> GetEditedClocksRegion(string region, int weekOfYr);
        Task<List<vw_EditedClocks>> GetEditedClocksDivision(string division, int weekOfYr);
        Task<List<vw_EditedClocks>> GetEditedClocksChain(string chain, int weekOfYr);
    }
}