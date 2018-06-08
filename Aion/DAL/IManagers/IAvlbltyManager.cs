using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IAvlbltyManager
    {
        Task<List<vw_AvailabilityPattern>> GetAllPatternsStore(string storeNum);
        Task<List<vw_AvailabilityPattern>> GetAllPatternsPerson(string personNumber);
        Task<List<AvailabilityStore>> GetAllPatternStoresPerson(string personNumber);
        Task<List<vw_AvailabilityPatternMissing>> GetAllColleaguesWithoutPattern(string storeNum);
        Task<bool> SubmitNewPattern(AvailabilityPattern newPattern, short[] storeList, string userName);
        Task<List<vw_AvailabilitySummary>> GetPatternStore(string storeNum);
    }
}