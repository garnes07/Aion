using Aion.DAL.Entities;
using Aion.ViewModels;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class MyTeamVm : BaseVm
    {
        public vw_ContractBaseDetail ContractBaseDetailStore { get; set; }
        public List<vw_ContractBaseDetail> ContractBaseDetailRegion { get; set; }
        public List<WFM_EMPLOYEE_INFO> StaffList { get; set; }
    }
}