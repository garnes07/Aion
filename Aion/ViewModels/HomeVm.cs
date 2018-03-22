using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aion.ViewModels
{
    public class HomeVm : BaseVm
    {
        public List<Activity> TopNews { get; set; }
        public List<vw_4WeekSummary> ScoreSummary { get; set; }
    }
}