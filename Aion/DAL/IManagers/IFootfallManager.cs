using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IFootfallManager
    {
        Task<List<vw_FootfallHourly>> GetFootfallStore(string store, string year, int weeknumber);
        Task<List<vw_FootfallHourly>> GetFootfallRegion(string region, string year, int weeknumber);
        Task<List<vw_FootfallHourly>> GetFootfallDivision(string division, string year, int weeknumber);
        Task<List<vw_FootfallHourly>> GetFootfallChain(string chain, string year, int weeknumber);
    }
}