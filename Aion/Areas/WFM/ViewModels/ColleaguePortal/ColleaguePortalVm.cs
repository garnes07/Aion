using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Aion.Areas.WFM.ViewModels.ColleaguePortal
{
    public class ColleaguePortalVm : BaseVm
    {
        public List<PayCalendarRefView> rawMenu { get; set; }
        public List<AvailabilityPatternView> avlbltyPattern { get; set; }

        private AvailabilityPatternView _defaultPattern;
        public AvailabilityPatternView DefaultPattern => _defaultPattern ?? (_defaultPattern = avlbltyPattern.FirstOrDefault(x => x.PatternType == 0));

        public bool avlbltlyPilot { get; set; }

        public List<SelectListItem> menu =>
            rawMenu.Select(x => new SelectListItem
            {
                Text = string.Format("{0} - Paid: {1:D}", x.Period, x.PayDate),
                Value = string.Format("{0}_{1}", x.Year, x.Period)
            }).ToList();
    }
}