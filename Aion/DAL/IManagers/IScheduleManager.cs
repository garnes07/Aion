using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IScheduleManager
    {
        Task<List<vw_ScheduleData>> GetStoreSchedule(string store, int weekNumber);
        Task<List<vw_ScheduleData>> GetRegionGMScheduleData(string region, int weekNumber);
    }
}