using Aion.Areas.WFM.Models.RFTP;
using Aion.Models.WFM;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.RFTPTracking
{
    public class IDDVm
    {
        public List<RFTPCaseStubView> historicCases { get; set; }
        public CompSummaryView lastPeriod { get; set; }
        public KronosEmployeeSummaryView empDetails { get; set; }
    }
}