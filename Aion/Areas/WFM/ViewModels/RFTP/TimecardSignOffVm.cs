using Aion.DAL.Entities;
using Aion.Helpers;
using Aion.Models.Kronos;
using Aion.ViewModels;
using System;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class TimecardSignOffVm : BaseVm
    {
        public List<HyperFindResult> hf { get; set; }
        public List<ShortShift> ss { get; set; }
        public List<sp_CheckHelpTickets_Result> HelpTcks { get; set; }
        public DateTime weekStart { get; set; }

        public TimecardSignOffVm()
        {
            weekStart = DateTime.Now.AddDays(-7).FirstDayOfWeek().Date;
        }
    }
}