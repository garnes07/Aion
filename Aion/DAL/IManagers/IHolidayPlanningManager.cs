using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IHolidayPlanningManager
    {
        Task<List<vw_HolidayPlanningStore>> GetHolidayStore(string store, int startWeek, int endWeek);
        Task<List<vw_HolidayPlanningStore>> GetHolidayRegion(string region, int startWeek, int endWeek);
        Task<List<vw_HolidayPlanningStore>> GetHolidayDivision(string division, int startWeek, int endWeek);
        Task<List<HolidayPlanningEmp>> GetHolidayStoreEmp(string store, int startWeek);
        Task<List<vw_HolidayPlanningRollup>> GetHolidayRegionRollup(string region, int startWeek);
        Task<List<vw_HolidayPlanningRollup>> GetHolidayDivisionRollup(string division, int startWeek);
    }
}