using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class ClockBehavioursVm : BaseVm
    {
        public List<CPW_Clocking_Data_TrendView> PunchDetail { get; set; }
        public List<CPW_Clocking_Repeat_StoresView> RepeatStoresDetail { get; set; }
        public List<CPW_Clocking_Repeat_EmployeesView> RepeatEmployeeDetail { get; set; }

        public List<int?> WeekNumbers => PunchDetail.GroupBy(x => x.FNCL_WK_NUM).Select(x => x.Key).OrderBy(x => x).ToList();
        public List<short?> StoreList => PunchDetail.GroupBy(x => x.StoreNumber).OrderBy(x => x.Key).Select(x => x.Key).Where(x => x != null).ToList();
        public List<short?> RegionList => PunchDetail.GroupBy(x => x.Region).Select(x => x.Key).Where(x => x != null).OrderBy(x => x).ToList();
        public List<string> DivisionList => PunchDetail.GroupBy(x => x.Division).Select(x => x.Key).Where(x => x != null).OrderBy(x => x).ToList();

        public List<CPW_Clocking_Data_TrendView> StoreSummary => PunchDetail.Where(x => x.StoreNumber != null).ToList();
        public List<CPW_Clocking_Data_TrendView> RegionSummary => PunchDetail.Where(x => x.Region != null && x.StoreNumber == null).ToList();
        public List<CPW_Clocking_Data_TrendView> DivisionSummary => PunchDetail.Where(x => x.Division != null && x.Region == null).ToList();
        public List<CPW_Clocking_Data_TrendView> ChainSummary => PunchDetail.Where(x => x.Chain != null && x.Division == null).ToList();

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