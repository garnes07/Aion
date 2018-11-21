using System;
using System.Collections.Generic;

namespace Aion.Models.WFM
{
    public class PayCalendarRefView
    {
        public PayCalendarRefView()
        {
            this.PayCalendarDates = new HashSet<PayCalendarDateView>();
        }

        public string Chain { get; set; }
        public string Year { get; set; }
        public string Period { get; set; }
        public DateTime PayDate { get; set; }
        
        public virtual ICollection<PayCalendarDateView> PayCalendarDates { get; set; }
    }
}