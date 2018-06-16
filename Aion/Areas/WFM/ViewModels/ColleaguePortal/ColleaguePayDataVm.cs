using Aion.DAL.Entities;
using Aion.Models.Kronos;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.ColleaguePortal
{
    public class ColleaguePayDataVm
    {
        public List<Timesheet> tSheet { get; set; }
        public List<vw_CPW_Clocking_Data> punch { get; set; }
        public List<PayCalendarDate> payDates { get; set; }

        public bool errorPayroll { get; set; }
    }
}