using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class EditedClocksVm : BaseVm
    {
        public List<EditedClockView> StoreDetail { get; set; }
        public List<AggEditedClocksView> AggregateDetail { get; set; }

        public List<string> PersonList => StoreDetail.GroupBy(x => x.PersonName).Select(x => x.Key).OrderBy(x => x).ToList();

        public List<AggEditedClocksView> DivisionSummary => AggregateDetail.Where(x => x.Division != null && x.Region == null).OrderBy(x => x.Division).ToList();
        public List<AggEditedClocksView> RegionSummary => AggregateDetail.Where(x => x.Region != null && x.StoreNumber == null).OrderBy(x => x.Region).ToList();

        public List<EditedClockView> PersonDetail(string _PersonName)
        {
            return StoreDetail.Where(x => x.PersonName == _PersonName).OrderBy(x => x.PunchDate).ToList();
        }
    }
}