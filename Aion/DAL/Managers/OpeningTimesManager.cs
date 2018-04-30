using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class OpeningTimesManager : IOpeningTimesManager
    {
        public async Task<List<StoreOpeningTime>> GetOpeningTimesStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.StoreOpeningTimes.Where(x =>
                    x.StoreNumber == crit && (x.Status == "Live" || x.Status.StartsWith("Pending") ||
                                              x.Status.StartsWith("Peak"))).ToListAsync();
            }
        }

        public async Task<StoreOpeningTime> GetCurrentOpeningTimeStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.StoreOpeningTimes.Where(x => x.StoreNumber == crit && x.Status == "Live")
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<List<StoreOpeningTime>> GetSpecificStoreOpeningTime(string store, DateTime sDate)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.StoreOpeningTimes.Where(x => x.StoreNumber == crit && ((x.EffectiveDate == sDate && x.Status != "Cancelled") || x.Status == "Live")).ToListAsync();
            }
        }

        public async Task<int> SubmitOpeningTimeChange(StoreOpeningTime entry, string userName)
        {
            using (var context = new WFMModel())
            {
                try
                {
                    entry.SubmittedByUser = userName;
                    entry.DateTimeSubmitted = DateTime.Now;
                    entry.Status = "PendingApproval";

                    context.StoreOpeningTimes.Add(entry);
                    return await context.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    return -5;
                }
            }
        }

        public async Task<StoreOpeningTime> GetSinglePendingById(int entryId, string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.StoreOpeningTimes
                    .Where(x => x.EntryID == entryId && x.StoreNumber == crit && x.Status.StartsWith("Pending"))
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<StoreOpeningTime> GetSinglePeakById(int entryId, string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.StoreOpeningTimes
                    .Where(x => x.EntryID == entryId && x.StoreNumber == crit && x.Status.StartsWith("Peak"))
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<int> CancelPendingChange(int entryId, string userName)
        {
            using (var context = new WFMModel())
            {
                var entry = await context.StoreOpeningTimes.FindAsync(entryId);
                if (entry == null || !entry.Status.StartsWith("Pending")) return -5;
                entry.Status = "Cancelled";
                entry.ModifiedByUser = userName;
                entry.DateTimeModified = DateTime.Now;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> EditExistingPeak(StoreOpeningTime newEntry, string userName)
        {
            using (var context = new WFMModel())
            {
                try
                {
                    newEntry.DateTimeModified = DateTime.Now;
                    newEntry.ModifiedByUser = userName;
                    newEntry.Status = "PeakPending";

                    context.StoreOpeningTimes.Attach(newEntry);
                    var entry = context.Entry(newEntry);
                    entry.State = EntityState.Modified;
                    
                    entry.Property(x => x.StoreNumber).IsModified = false;
                    entry.Property(x => x.DateTimeSubmitted).IsModified = false;
                    entry.Property(x => x.EffectiveDate).IsModified = false;
                    entry.Property(x => x.TemporaryChange).IsModified = false;
                    entry.Property(x => x.EndDate).IsModified = false;
                    entry.Property(x => x.SubmittedByUser).IsModified = false;
                    entry.Property(x => x.ReasonForChange).IsModified = false;

                    return await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return -5;
                }
            }
        }

        public async Task<int> AcceptPeak(int entryId, string store, string userName)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                var existing = await context.StoreOpeningTimes.Where(x => x.EntryID == entryId && x.StoreNumber == crit)
                    .FirstOrDefaultAsync();

                if (existing != null)
                {
                    existing.Status = "PeakConfirmed";
                    existing.ModifiedByUser = userName;
                    existing.DateTimeModified = DateTime.Now;

                    return await context.SaveChangesAsync();
                }

                return -5;
            }
        }

        public async Task<List<StoreOpeningTime>> GetPendingOpeningTimes()
        {
            using (var context = new WFMModel())
            {
                return await context.StoreOpeningTimes.Where(x => x.Status == "PendingApproval").OrderBy(x => x.StoreNumber).ToListAsync();
            }
        }

        public async Task<List<StoreOpeningTime>> GetStoreTimesForReview(int storeNumber)
        {
            using (var context = new WFMModel())
            {
                return await context.StoreOpeningTimes.Where(x => x.StoreNumber == storeNumber && (x.Status == "PendingApproval" || x.Status == "Pending" || x.Status == "Live")).Include("OpeningTimesComments").ToListAsync();
            }
        }

        public async Task<bool> ApproveRejectPendingRequest(int entryId, bool approved)
        {
            using (var context = new WFMModel())
            {
                try
                {
                    var result = context.StoreOpeningTimes.Find(entryId);
                    if(result != null)
                    {
                        result.Status = approved ? "Pending" : "Declined";
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<OpeningTimesComment> AddNewComment(int entryId, string commentText, string userName)
        {
            using (var context = new WFMModel())
            {
                var _toAttach = new OpeningTimesComment
                {
                    EntryID = entryId,
                    Comment = commentText,
                    AddedBy = userName,
                    Datetime = DateTime.Now
                };

                context.OpeningTimesComments.Add(_toAttach);
                await context.SaveChangesAsync();
                return _toAttach;
            }
        }
    }
}