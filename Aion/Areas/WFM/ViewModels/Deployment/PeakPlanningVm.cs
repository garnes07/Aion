using Aion.ViewModels;
using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class PeakPlanningVm : BaseVm
    {
        public List<PeakData> collection { get; set; }

        public bool tempsReq => collection.FirstOrDefault()?.Temps != 0;
    }
}