using Aion.DAL.Entities;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class CompDetailVm : BaseVm
    {
        public List<vw_DashboardData_v2> _dashboardView { get; set; }
        public List<sp_AllChainDashboardData_v2_Result> _chainView { get; set; }
        public List<ShortShiftDetail> _ssDetails { get; set; }
        public List<PunchCompDetail> _punchDetails { get; set; }
        public List<TimecardDetail> _timecardDetails { get; set; }
        
        public void loadTimecardDetails(List<EmpComplianceDetail> a)
        {
            _timecardDetails = a.OrderBy(x => x.PersonName).Select(x => new TimecardDetail
            {
                empName = x.PersonName,
                empNumber = x.PersonNumber,
                type = !x.TimecardSignedOff ? "Not Submitted" : "Zero Hours"
            }).ToList();
        }

        public void loadPunchDetails(List<vw_CPW_Clocking_Data> a)
        {
            var temp = a.GroupBy(x => new {x.FORENAME, x.EMP_NUM}).Select(s => new
            {
                EmpNum = s.Key.EMP_NUM,
                FullName = s.Key.FORENAME,
                ShiftCount = s.Count(),
                MissedIn = s.Sum(x => x.Count_In_Missing),
                MissedOut = s.Sum(x => x.Count_Out_Missing),
                LateIn = s.Sum(x => x.Clock_In_Not_Acceptable)
            }).OrderBy(x => x.FullName).ToList();
            _punchDetails = temp.Select(x => new PunchCompDetail { PersonName = x.FullName, PunchComp = 100 - (((decimal)x.MissedIn + x.MissedOut) / x.ShiftCount) * 50 }).ToList();
        }

        public void loadSSDetails(List<ShortShift> a)
        {
            var temp = a.GroupBy(x => new { x.PersonName }).Select(s => new { PersonName = s.Key.PersonName, ShortShifts = s.Count() }).OrderBy(x => x.PersonName).ToList();
            _ssDetails = temp.Select(x => new ShortShiftDetail { PersonName = x.PersonName, ShortShifts = x.ShortShifts }).ToList();
        }        
    }    

    public class ShortShiftDetail
    {
        public string PersonName { get; set; }
        public int ShortShifts { get; set; }
    }

    public class PunchCompDetail
    {
        public string PersonName { get; set; }
        public decimal PunchComp { get; set; }
    }

    public class TimecardDetail
    {
        public string empName { get; set; }
        public string empNumber { get; set; }
        public string type { get; set; }
    }
}