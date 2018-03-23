using Aion.DAL.Entities;
using Aion.Models.Kronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class ColleaguePayDataVm
    {
        public List<Timesheet> tSheet { get; set; }
        public List<vw_CPW_Clocking_Data> punch { get; set; }
        public List<PayCalendarDate> payDates { get; set; }

        public bool errorPayroll { get; set; }
    }
}