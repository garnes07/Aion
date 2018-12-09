using Aion.Areas.WFM.Models.MyStore;
using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> SubmitOpeningTimeChange(NewOpeningTime entry, string userName)
        {
            using (var context = new WFMModel())
            {
                try
                {
                    StoreOpeningTime toAdd = new StoreOpeningTime
                    {
                        StoreNumber = entry.StoreNumber,
                        DateTimeSubmitted = DateTime.Now,
                        EffectiveDate = entry.EffectiveDate,
                        Status = "PendingApproval",
                        SubmittedByUser = userName
                    };

                    toAdd = MapToStoreOpeningTime(entry, toAdd);

                    context.StoreOpeningTimes.Add(toAdd);
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

        public async Task<int> EditExistingPeak(NewOpeningTime newEntry, string userName)
        {
            using (var context = new WFMModel())
            {
                try
                {
                    var toEdit = await context.StoreOpeningTimes.FindAsync(newEntry.EntryId);

                    toEdit.DateTimeModified = DateTime.Now;
                    toEdit.ModifiedByUser = userName;
                    toEdit.Status = "PeakPending";

                    toEdit = MapToStoreOpeningTime(newEntry, toEdit);

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

        private StoreOpeningTime MapToStoreOpeningTime(NewOpeningTime toMap, StoreOpeningTime container)
        {
            StoreOpeningTime toReturn = container;

            toReturn.SundayOpen = toMap.SundayOpen;
            toReturn.SundayClose = toMap.SundayClose;
            toReturn.MondayOpen = toMap.MondayOpen;
            toReturn.MondayClose = toMap.MondayClose;
            toReturn.TuesdayOpen = toMap.TuesdayOpen;
            toReturn.TuesdayClose = toMap.TuesdayClose;
            toReturn.WednesdayOpen = toMap.WednesdayOpen;
            toReturn.WednesdayClose = toMap.WednesdayClose;
            toReturn.ThursdayOpen = toMap.ThursdayOpen;
            toReturn.ThursdayClose = toMap.ThursdayClose;
            toReturn.FridayOpen = toMap.FridayOpen;
            toReturn.FridayClose = toMap.FridayClose;
            toReturn.SaturdayOpen = toMap.SaturdayOpen;
            toReturn.SaturdayClose = toMap.SaturdayClose;

            return toReturn;
        }
    }
}