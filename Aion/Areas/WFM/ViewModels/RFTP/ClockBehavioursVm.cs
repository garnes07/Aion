using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aion.DAL.Entities;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class ClockBehavioursVm : BaseVm
    {
        public List<vw_CPW_Clocking_Data> PunchDetail { get; set; }
    }
}