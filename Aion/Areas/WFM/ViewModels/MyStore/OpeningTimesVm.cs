using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.MyStore
{
    public class OpeningTimesVm : BaseVm
    {
        public List<StoreOpeningTimeView> Collection { get; set; }

        public StoreOpeningTimeView _CurrentTime;
        public StoreOpeningTimeView CurrentTime => _CurrentTime ?? (_CurrentTime = Collection.FirstOrDefault(x => x.Status == "Live"));

        public List<StoreOpeningTimeView> PendingTimes => Collection != null ? Collection.Where(x => x.Status.StartsWith("Pending")).OrderBy(x => x.EffectiveDate).ToList() : new List<StoreOpeningTimeView>();
        public List<StoreOpeningTimeView> PeakTimes => Collection != null ? Collection.Where(x => x.Status.StartsWith("Peak")).OrderBy(x => x.EffectiveDate).ToList(): new List<StoreOpeningTimeView>();
    }
}