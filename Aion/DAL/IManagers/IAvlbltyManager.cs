using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IAvlbltyManager
    {
        Task<List<vw_AvailabilityPattern>> GetAllPatternsStore(string storeNum);
        Task<List<vw_AvailabilityPattern>> GetAllPatternsPerson(string personNumber);
        Task<List<vw_AvailabilityPattern>> GetPatternsForAvailableCover(string storeNum);
        Task<AvailabilityContact> GetContactDetailsPerson(string personNumber);
        Task<vw_TradingHoursForAvlblty> GetCurrentTradingHrs(string storeNum);
        Task<List<vw_AvailabilityCompletionRate>> GetCompletionRateRegion(string region);
        Task<List<AvailabilityStore>> GetAllPatternStoresPerson(string personNumber);
        Task<List<vw_AvailabilityPatternMissing>> GetAllColleaguesWithoutPattern(string storeNum);
        Task<bool> SubmitNewPattern(AvailabilityPattern newPattern, short[] storeList, string userName);
        Task<List<vw_AvailabilitySummary>> GetPatternStore(string storeNum);
        Task<bool> CheckAddRightsForUser(string personNumber, string storeNumber);
        Task<bool> UpdateContactDetails(AvailabilityContact ContactDetail, bool deleteRecord, string personNumber, bool confirmOnly);
    }
}