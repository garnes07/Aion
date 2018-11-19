using System;

namespace Aion.Models.WebMaster
{
    public class ActivityView
    {
        public long Activityid { get; set; }
        public string ActivityName { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? ActivityTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int? StoreNumber { get; set; }
        public short? PriorityTypeId { get; set; }
    }
}