using System;
using System.Collections.Generic;

namespace Aion.Models.WFM
{
    public class AvailabilityPatternView
    {
        public AvailabilityPatternView()
        {
            this.AvailabilityDays = new HashSet<AvailabilityDayView>();
        }

        public int PatternId { get; set; }
        public short? HomeBranch { get; set; }
        public string PersonNumber { get; set; }
        public string PersonName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastConfirmedDate { get; set; }
        public string LastConfirmedBy { get; set; }
        public int PatternType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        
        public virtual ICollection<AvailabilityDayView> AvailabilityDays { get; set; }
    }
}