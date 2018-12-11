using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class ScheduleVm : BaseVm
    {
        public List<ScheduleDataView> Collection { get; set; }
        public StoreOpeningTimeView OpeningTime { get; set; }
        public string HomeStore { get; set; } 

        public List<int> ColleagueList => Collection.GroupBy(x => x.PersonNumber).Select(x => x.Key.Value).ToList();

        private List<ScheduleDataView> _SOHTotal;
        public List<ScheduleDataView> SOHTotal => _SOHTotal ?? (_SOHTotal = Collection
                                                     .Where(x => x.TransferBranch == null || x.TransferBranch == HomeStore).GroupBy(x => x.PersonNumber)
                                                     .Select(x => x.FirstOrDefault()).ToList());
    }
}