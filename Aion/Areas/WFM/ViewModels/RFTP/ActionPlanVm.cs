using Aion.Models.WFM;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class ActionPlanVm
    {
        public List<ActionPlanView> actions { get; set; }
        public bool SWAS { get; set; }
        public bool newSubmission { get; set; }
    }
}