using System;
using System.Collections.Generic;

namespace Aion.Models.WFM
{
    public class StoreOpeningTimeView
    {
        public StoreOpeningTimeView()
        {
            this.OpeningTimesComments = new HashSet<OpeningTimesCommentView>();
        }

        public int EntryID { get; set; }
        public int StoreNumber { get; set; }
        public DateTime DateTimeSubmitted { get; set; }
        public TimeSpan SundayOpen { get; set; }
        public TimeSpan SundayClose { get; set; }
        public TimeSpan MondayOpen { get; set; }
        public TimeSpan MondayClose { get; set; }
        public TimeSpan TuesdayOpen { get; set; }
        public TimeSpan TuesdayClose { get; set; }
        public TimeSpan WednesdayOpen { get; set; }
        public TimeSpan WednesdayClose { get; set; }
        public TimeSpan ThursdayOpen { get; set; }
        public TimeSpan ThursdayClose { get; set; }
        public TimeSpan FridayOpen { get; set; }
        public TimeSpan FridayClose { get; set; }
        public TimeSpan SaturdayOpen { get; set; }
        public TimeSpan SaturdayClose { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool? TemporaryChange { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public string ModifiedByUser { get; set; }
        public string SubmittedByUser { get; set; }
        public string ReasonForChange { get; set; }
        
        public virtual ICollection<OpeningTimesCommentView> OpeningTimesComments { get; set; }
    }
}