using Aion.Areas.WFM.Models.MyStore;
using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public interface IOpeningTimesManager
    {
        Task<List<StoreOpeningTime>> GetOpeningTimesStore(string store);
        Task<StoreOpeningTime> GetCurrentOpeningTimeStore(string store);
        Task<List<StoreOpeningTime>> GetSpecificStoreOpeningTime(string store, DateTime sDate);
        Task<int> SubmitOpeningTimeChange(NewOpeningTime entry, string userName);
        Task<StoreOpeningTime> GetSinglePendingById(int entryId, string store);
        Task<StoreOpeningTime> GetSinglePeakById(int entryId, string store);
        Task<int> CancelPendingChange(int entryId, string userName);
        Task<int> EditExistingPeak(NewOpeningTime newEntry, string userName);
        Task<int> AcceptPeak(int entryId, string store, string userName);
        Task<List<StoreOpeningTime>> GetPendingOpeningTimes();
        Task<List<StoreOpeningTime>> GetStoreTimesForReview(int storeNumber);
        Task<bool> ApproveRejectPendingRequest(int entryId, bool approved);
        Task<OpeningTimesComment> AddNewComment(int entryId, string commentText, string userName);
    }
}