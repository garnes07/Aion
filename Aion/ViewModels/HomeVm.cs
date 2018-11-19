using Aion.Models.WebMaster;
using System.Collections.Generic;

namespace Aion.ViewModels
{
    public class HomeVm : BaseVm
    {
        public List<ActivityView> TopNews { get; set; }
        public List<SummaryView> ScoreSummary { get; set; }
    }
}