﻿using Aion.Helpers;
using Aion.Models.Kronos;
using Aion.Models.WebMaster;
using Aion.Models.WFM;
using Aion.ViewModels;
using System;
using System.Collections.Generic;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class TimecardSignOffVm : BaseVm
    {
        public List<HyperFindResult> hf { get; set; }
        public List<EditedClockView> ss { get; set; }
        public List<CheckHelpTicketsView> HelpTcks { get; set; }
        public DateTime weekStart { get; set; }
        public List<RegionSignOff> rso { get; set; }

        public TimecardSignOffVm()
        {
            weekStart = DateTime.Now.AddDays(-7).FirstDayOfWeek().Date;
        }
    }

    public class RegionSignOff
    {
        public short? BranchNumber { get; set; }
        public string BranchName { get; set; }
        public int SignedOff { get; set; }
        public int Headcount { get; set; }
        public bool KronosScheduled { get; set; }
        public bool KronosPunched { get; set; }

        public List<string> EmpIssues { get; set; }

        public short RAG
        {
            get
            {
                if (SignedOff >= Headcount) return 0;
                if (!KronosScheduled && !KronosPunched)
                {
                    return 3;
                }

                if (!KronosScheduled || !KronosPunched)
                {
                    return 2;
                }

                return 1;

            }
        }

        public string RAGClass
        {
            get
            {
                if (SignedOff >= Headcount) return "";
                if (!KronosScheduled && !KronosPunched)
                {
                    return "table-danger";
                }

                if (!KronosScheduled || !KronosPunched)
                {
                    return "table-warning";
                }

                return "";
            }
        }
    }
}