using System.Collections.Generic;
using System.Linq;
using Aion.DAL.Entities;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.RFTPTracking
{
    public class RFTPManagerTrendVm : BaseVm
    {
        public List<vw_Last12MonthRFTPCases> Cases { get; set; }
        public List<Last12MonthList> PeriodList { get; set; }
        public List<KronosEmployeeSummary> EmployeeList { get; set; }

        public List<KronosEmployeeSummary> gmList => EmployeeList.Where(x => x.ReportingRoleFlag == 1 && x.Active == true).OrderBy(x => x.PersonName).ToList();
        public List<KronosEmployeeSummary> nonGMList => EmployeeList.Where(x => x.ReportingRoleFlag != 1 && x.Active == true).OrderBy(x => x.PersonName).ToList();
    }
}