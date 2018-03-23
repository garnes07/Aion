using Aion.DAL.Entities;
using Aion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class ColleaguePortalVm : BaseVm
    {
        public List<PayCalendarRef> rawMenu { get; set; }

        public List<SelectListItem> menu =>
            rawMenu.Select(x => new SelectListItem
            {
                Text = string.Format("{0} - Paid: {1:D}", x.Period, x.PayDate),
                Value = string.Format("{0}_{1}", x.Year, x.Period)
            }).ToList();
    }
}