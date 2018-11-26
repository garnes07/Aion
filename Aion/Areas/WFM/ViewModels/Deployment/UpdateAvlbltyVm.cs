using Aion.Models.WFM;
using Aion.Helpers;
using Aion.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Aion.Models.WebMaster;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class UpdateAvlbltyVm : BaseVm
    {
        public List<AvailabilityPatternView> existingPattern { get; set; }
        public KronosEmployeeSummaryView empDetails { get; set; }
        public List<StoreMasterView> LocalStores { get; set; }
        public List<AvailabilityStoreView> AvlbltyStores { get; set; }
        public AvailabilityContactView contactDetails { get; set; }
        public bool displayContact { get; set; }

        public UpdateAvlbltyVm()
        {
            displayContact = false;
        }

        public string StoreList => AvlbltyStores.Count() == 0 ? "[]" : JsonConvert.SerializeObject(AvlbltyStores.SelectMany(x => new[] { x.StoreNumber }));

        private IEnumerable<SelectListItem> _times;
        public IEnumerable<SelectListItem> Times => _times ?? (_times = VmHelpers.GetTimes("Unavailable"));

        private AvailabilityPatternView _defaultPattern;
        public AvailabilityPatternView DefaultPattern => _defaultPattern ?? (_defaultPattern = existingPattern.FirstOrDefault(x => x.PatternType == 0));
    }
}