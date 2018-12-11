using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.RFTPTracking
{
    public class RFTPManagerTrendVm : BaseVm
    {
        public List<Last12MonthRFTPCasesView> Cases { get; set; }
        public List<Last12MonthListView> PeriodList { get; set; }
        public List<KronosEmployeeSummaryView> EmployeeList { get; set; }

        public List<KronosEmployeeSummaryView> gmList => EmployeeList.Where(x => x.ReportingRoleFlag == 1 && x.Active == true).OrderBy(x => x.PersonName).ToList();
        public List<KronosEmployeeSummaryView> nonGMList => EmployeeList.Where(x => x.ReportingRoleFlag != 1 && x.Active == true).OrderBy(x => x.PersonName).ToList();
    }
}