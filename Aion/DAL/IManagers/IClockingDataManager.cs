using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IClockingDataManager
    {
        Task<List<vw_CPW_Clocking_Data>> GetClockDetailStore(string store, int weekOfYr);
        Task<List<vw_CPW_Clocking_Data>> GetClockDetailRegion(string region, int weekOfYr);
        Task<List<vw_CPW_Clocking_Data>> GetClockDetailDivision(string division, int weekOfYr);
        Task<List<vw_CPW_Clocking_Data>> GetClockDetailChain(string chain, int weekOfYr);
    }
}