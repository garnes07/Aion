using Aion.DAL.Entities;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class ActionPlanVm
    {
        public List<vw_ActionPlan> actions { get; set; }
        public bool SWAS { get; set; }
        public bool newSubmission { get; set; }
    }
}