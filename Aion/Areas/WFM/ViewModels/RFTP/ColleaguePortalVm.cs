using Aion.DAL.Entities;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class ColleaguePortalVm : BaseVm
    {
        public List<PayCalendarRef> rawMenu { get; set; }
        public List<vw_AvailabilityPattern> avlbltyPattern { get; set; }

        private vw_AvailabilityPattern _defaultPattern;
        public vw_AvailabilityPattern DefaultPattern => _defaultPattern ?? (_defaultPattern = avlbltyPattern.FirstOrDefault(x => x.PatternType == 0));

        public bool avlbltlyPilot { get; set; }

        public List<SelectListItem> menu =>
            rawMenu.Select(x => new SelectListItem
            {
                Text = string.Format("{0} - Paid: {1:D}", x.Period, x.PayDate),
                Value = string.Format("{0}_{1}", x.Year, x.Period)
            }).ToList();
    }
}