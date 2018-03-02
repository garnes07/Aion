using System.Collections.Generic;
using Aion.DAL.Entities;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.RFTPTracking
{
    public class RFTPManagerDetailVm : BaseVm
    {
        public List<RFTPCaseStub> caseDetails { get; set; }
    }
}