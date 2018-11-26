using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class MyTeamVm : BaseVm
    {
        public ContractBaseDetailView ContractBaseDetailStore { get; set; }
        public List<ContractBaseDetailView> ContractBaseDetailRegion { get; set; }
        public List<WFMEmployeeInfoView> StaffList { get; set; }
    }
}