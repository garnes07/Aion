using System.Collections.Generic;
using System.Linq;
using Aion.DAL.Entities;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.MyStore
{
    public class ScheduleVm : BaseVm
    {
        public List<vw_ScheduleData> Collection { get; set; }
        public StoreOpeningTime OpeningTime { get; set; }

        public List<int> ColleagueList => Collection.GroupBy(x => x.PersonNumber).Select(x => x.Key.Value).ToList();

        private List<vw_ScheduleData> _SOHTotal;
        public List<vw_ScheduleData> SOHTotal => _SOHTotal ?? (_SOHTotal = Collection
                                                     .Where(x => x.TransferBranch == null).GroupBy(x => x.PersonNumber)
                                                     .Select(x => x.FirstOrDefault()).ToList());
    }
}