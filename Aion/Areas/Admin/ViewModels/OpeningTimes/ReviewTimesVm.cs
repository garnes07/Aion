using Aion.Models.WFM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.Admin.ViewModels.OpeningTimes
{
    public class ReviewTimesVm
    {
        public StoreOpeningTimeView currentTime { get; set; }
        public StoreOpeningTimeView timeForApproval { get; set; }
        public List<StoreOpeningTimeView> pendingTimes { get; set; }
        public List<TimeSpan> Differences { get; set; }

        public ReviewTimesVm(int entryId, List<StoreOpeningTimeView> collection)
        {
            currentTime = collection.FirstOrDefault(x => x.Status == "Live");
            timeForApproval = collection.FirstOrDefault(x => x.EntryID == entryId);
            pendingTimes = collection.Where(x => x.Status.Contains("Pending") && x.EntryID != entryId).ToList();

            Differences = new List<TimeSpan>();
            Differences.Add(timeForApproval.SundayClose.Subtract(timeForApproval.SundayOpen).Subtract(currentTime.SundayClose.Subtract(currentTime.SundayOpen)));
            Differences.Add(timeForApproval.MondayClose.Subtract(timeForApproval.MondayOpen).Subtract(currentTime.MondayClose.Subtract(currentTime.MondayOpen)));
            Differences.Add(timeForApproval.TuesdayClose.Subtract(timeForApproval.TuesdayOpen).Subtract(currentTime.TuesdayClose.Subtract(currentTime.TuesdayOpen)));
            Differences.Add(timeForApproval.WednesdayClose.Subtract(timeForApproval.WednesdayOpen).Subtract(currentTime.WednesdayClose.Subtract(currentTime.WednesdayOpen)));
            Differences.Add(timeForApproval.ThursdayClose.Subtract(timeForApproval.ThursdayOpen).Subtract(currentTime.ThursdayClose.Subtract(currentTime.ThursdayOpen)));
            Differences.Add(timeForApproval.FridayClose.Subtract(timeForApproval.FridayOpen).Subtract(currentTime.FridayClose.Subtract(currentTime.FridayOpen)));
            Differences.Add(timeForApproval.SaturdayClose.Subtract(timeForApproval.SaturdayOpen).Subtract(currentTime.SaturdayClose.Subtract(currentTime.SaturdayOpen)));
            var total = Differences.Sum(x => x.TotalMinutes);
            Differences.Add(TimeSpan.FromMinutes(total));
        }
    }
}