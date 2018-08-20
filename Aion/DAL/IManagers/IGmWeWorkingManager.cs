using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IGmWeWorkingManager
    {
        Task<List<vw_GmWeWorking>> GetGmWorkingRegionUsingStore(string store);
        Task<List<vw_GmWeWorking>> GetGmWorkingRegion(string regionNo);
        Task<List<vw_GmWeWorking>> GetGmWorkingDivision(string division);
        Task<List<vw_GmWeWorking>> GetGmWorkingChain(string chain);
        Task<List<GMPowerHour>> GetGMPowerHours(string store, int weekNum);
    }
}