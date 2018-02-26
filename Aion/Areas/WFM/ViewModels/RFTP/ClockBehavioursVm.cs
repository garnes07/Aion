using System.Collections.Generic;
using System.Linq;
using Aion.DAL.Entities;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class ClockBehavioursVm : BaseVm
    {
        public List<vw_CPW_Clocking_Data_Trend> PunchDetail { get; set; }
        public List<vw_CPW_Clocking_Repeat_Stores> RepeatStoresDetail { get; set; }
        public List<vw_CPW_Clocking_Repeat_Employees> RepeatEmployeeDetail { get; set; }

        public List<int?> WeekNumbers => PunchDetail.GroupBy(x => x.FNCL_WK_NUM).Select(x => x.Key).OrderBy(x => x).ToList();
        public List<short?> StoreList => PunchDetail.GroupBy(x => x.StoreNumber).OrderBy(x => x.Key).Select(x => x.Key).Where(x => x != null).ToList();
        public List<short?> RegionList => PunchDetail.GroupBy(x => x.Region).Select(x => x.Key).Where(x => x != null).OrderBy(x => x).ToList();
        public List<string> DivisionList => PunchDetail.GroupBy(x => x.Division).Select(x => x.Key).Where(x => x != null).OrderBy(x => x).ToList();

        public List<vw_CPW_Clocking_Data_Trend> StoreSummary => PunchDetail.Where(x => x.StoreNumber != null).ToList();
        public List<vw_CPW_Clocking_Data_Trend> RegionSummary => PunchDetail.Where(x => x.Region != null && x.StoreNumber == null).ToList();
        public List<vw_CPW_Clocking_Data_Trend> DivisionSummary => PunchDetail.Where(x => x.Division != null && x.Region == null).ToList();
        public List<vw_CPW_Clocking_Data_Trend> ChainSummary => PunchDetail.Where(x => x.Chain != null && x.Division == null).ToList();

        public List<RepeatOffender> RepeatStoresList => RepeatStoresDetail
            .GroupBy(x => new {x.StoreNumber, x.StoreName}).OrderByDescending(x => x.Average(y => y.Compliance))
            .Select(x => new RepeatOffender {StoreNumber = x.Key.StoreNumber, StoreName = x.Key.StoreName}).ToList();

        public List<RepeatOffender> RepeatEmployeeList => RepeatEmployeeDetail
            .GroupBy(x => new {x.Forename, x.StoreNumber, x.StoreName})
            .OrderByDescending(x => x.Average(y => y.Compliance))
            .Select(x => new RepeatOffender { Forename = x.Key.Forename, StoreNumber = x.Key.StoreNumber, StoreName = x.Key.StoreName}).ToList();
    }

    public class RepeatOffender
    {
        public string Forename { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }

        public string FullStoreName => string.Format("{0} - {1}", StoreNumber, StoreName);
    }
}