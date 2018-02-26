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
        Task<List<vw_CPW_Clocking_Data_Trend>> GetClockTrendStore(string store);
        Task<List<vw_CPW_Clocking_Data_Trend>> GetClockTrendRegion(string region);
        Task<List<vw_CPW_Clocking_Data_Trend>> GetClockTrendDivision(string division);
        Task<List<vw_CPW_Clocking_Data_Trend>> GetClockTrendChain(string chain);
        Task<List<vw_CPW_Clocking_Repeat_Employees>> GetRepeatOffendersStore(string store);
        Task<List<vw_CPW_Clocking_Repeat_Employees>> GetRepeatOffendersRegion(string region);
        Task<List<vw_CPW_Clocking_Repeat_Employees>> GetRepeatOffendersDivision(string division);
        Task<List<vw_CPW_Clocking_Repeat_Stores>> GetRepeatOffendersChain(string chain);
    }
}