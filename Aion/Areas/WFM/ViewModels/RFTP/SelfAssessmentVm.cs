using Aion.Areas.WFM.Models.RFTP;
using Aion.DAL.Entities;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class SelfAssessmentVm : BaseVm
    {
        public List<SASubmission> PastSubmissions { get; set; }
        public vw_SelfAssessmentRequired Requirement { get; set; }
        public List<CompSummaryView> Summary { get; set; }
        public bool errorPayroll { get; set; }

        public CompSummaryView PeriodSummary => Summary.FirstOrDefault(x => x.WeekNumber == null && x.StoreCount != null);
    }
}