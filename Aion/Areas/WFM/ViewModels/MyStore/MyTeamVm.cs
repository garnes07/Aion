using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aion.DAL.Entities;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.MyStore
{
    public class MyTeamVm : BaseVm
    {
        public vw_ContractBaseDetail ContractBaseDetailStore { get; set; }
        public List<vw_ContractBaseDetail> ContractBaseDetailRegion { get; set; }
        public List<HrFeed> StaffList { get; set; }
    }
}