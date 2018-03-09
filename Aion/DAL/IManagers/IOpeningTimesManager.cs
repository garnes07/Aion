using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IOpeningTimesManager
    {
        Task<List<StoreOpeningTime>> GetOpeningTimesStore(string store);
        Task<StoreOpeningTime> GetCurrentOpeningTimeStore(string store);
        Task<int> SubmitOpeningTimeChange(StoreOpeningTime entry, string userName);
        Task<StoreOpeningTime> GetSinglePendingById(int entryId, string store);
        Task<StoreOpeningTime> GetSinglePeakById(int entryId, string store);
        Task<int> CancelPendingChange(int entryId, string userName);
        Task<int> EditExistingPeak(StoreOpeningTime newEntry, string userName);
        Task<int> AcceptPeak(int entryId, string store, string userName);
    }
}