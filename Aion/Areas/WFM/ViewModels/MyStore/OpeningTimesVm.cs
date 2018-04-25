using System.Collections.Generic;
using System.Linq;
using Aion.DAL.Entities;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.MyStore
{
    public class OpeningTimesVm : BaseVm
    {
        public List<StoreOpeningTime> Collection { get; set; }

        public StoreOpeningTime _CurrentTime;
        public StoreOpeningTime CurrentTime => _CurrentTime ?? (_CurrentTime = Collection.FirstOrDefault(x => x.Status == "Live"));

        public List<StoreOpeningTime> PendingTimes => Collection != null ? Collection.Where(x => x.Status.StartsWith("Pending")).OrderBy(x => x.EffectiveDate).ToList() : new List<StoreOpeningTime>();
        public List<StoreOpeningTime> PeakTimes => Collection != null ? Collection.Where(x => x.Status.StartsWith("Peak")).OrderBy(x => x.EffectiveDate).ToList(): new List<StoreOpeningTime>();
    }
}