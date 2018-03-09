using System;
using System.ComponentModel.DataAnnotations;

namespace Aion.Areas.WFM.Models.MyStore
{
    public class NewOpeningTime
    {
        public int EntryId { get; set; }
        public DateTime DateTimeSubmitted { get; set; }
        public int StoreNumber { get; set; }
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
        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }
        public string SubmittedByUser { get; set; }
        [Display(Name = "Reason For Change"), Required]
        public string ReasonForChange { get; set; }
    }
}