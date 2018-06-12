using Aion.DAL.Entities;
using Aion.Helpers;
using Aion.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class UpdateAvlbltyVm : BaseVm
    {
        public List<vw_AvailabilityPattern> existingPattern { get; set; }
        public KronosEmployeeSummary empDetails { get; set; }
        public List<StoreMaster> LocalStores { get; set; }
        public List<AvailabilityStore> AvlbltyStores { get; set; }
        public AvailabilityContact contactDetails { get; set; }
        public bool displayContact { get; set; }

        public UpdateAvlbltyVm()
        {
            displayContact = false;
        }

        public string StoreList => AvlbltyStores.Count() == 0 ? "[]" : JsonConvert.SerializeObject(AvlbltyStores.SelectMany(x => new[] { x.StoreNumber }));

        private IEnumerable<SelectListItem> _times;
        public IEnumerable<SelectListItem> Times => _times ?? (_times = VmHelpers.GetTimes("Unavailable"));

        private vw_AvailabilityPattern _defaultPattern;
        public vw_AvailabilityPattern DefaultPattern => _defaultPattern ?? (_defaultPattern = existingPattern.FirstOrDefault(x => x.PatternType == 0));
    }
}