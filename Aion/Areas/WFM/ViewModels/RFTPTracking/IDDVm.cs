using Aion.Areas.WFM.Models.RFTP;
using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aion.Areas.WFM.ViewModels.RFTPTracking
{
    public class IDDVm
    {
        public List<RFTPCaseStub> historicCases { get; set; }
        public CompSummaryView lastPeriod { get; set; }
        public KronosEmployeeSummary empDetails { get; set; }
    }
}