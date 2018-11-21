using Aion.Models.Kronos;
using Aion.Models.WFM;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.ColleaguePortal
{
    public class ColleaguePayDataVm
    {
        public List<Timesheet> tSheet { get; set; }
        public List<CPW_Clocking_DataView> punch { get; set; }
        public List<PayCalendarDateView> payDates { get; set; }

        public bool errorPayroll { get; set; }
    }
}