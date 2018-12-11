using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.RFTPTracking
{
    public class RFTPManagerDetailVm : BaseVm
    {
        public List<RFTPCaseStubView> caseDetails { get; set; }
    }
}