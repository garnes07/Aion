using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Aion.Areas.WFM.Models.MyStore;
using Aion.Helpers;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.MyStore
{
    public class NewOpeningTimeVm : BaseVm
    {
        public NewOpeningTime NewTime { get; set; }

        private IEnumerable<SelectListItem> _times;
        public IEnumerable<SelectListItem> Times => _times ?? (_times = VmHelpers.GetTimes());

        private IEnumerable<SelectListItem> _weeks;
        public IEnumerable<SelectListItem> AvailableWeeks
        {
            get
            {
                if (_weeks != null)
                {
                    return _weeks;
                }
                DateTime CurrentDate = DateTime.Now;
                DateTime SundayDate = CurrentDate.AddDays(-(int)CurrentDate.DayOfWeek);

                List<SelectListItem> weeks = new List<SelectListItem>();
                for (int i = 0; i < 13; i++)
                {
                    weeks.Add(new SelectListItem { Text = SundayDate.ToShortDateString(), Value = SundayDate.ToShortDateString() });
                    SundayDate = SundayDate.AddDays(7);
                }

                return _weeks = new SelectList(weeks, "Value", "Text");
            }
        }
    }
}