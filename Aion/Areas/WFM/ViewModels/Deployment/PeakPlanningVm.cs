using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class PeakPlanningVm : BaseVm
    {
        public List<PeakDataView> collection { get; set; }

        public bool tempsReq => collection.FirstOrDefault()?.Temps != 0;
    }
}