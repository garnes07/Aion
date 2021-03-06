﻿using Aion.Areas.WFM.Models.RFTP;
using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class ClockCompVm : BaseVm
    {
        public List<CPW_Clocking_DataView> PunchDetail { get; set; }
        public string Priority { get; set; }

        public List<ClockAggregate> EmpSummary
        {
            get
            {
                return PunchDetail.GroupBy(x => new { x.FORENAME, x.EMP_NUM }).Select(x =>
                    new ClockAggregate
                    {
                        Name = x.Key.FORENAME,
                        empNum = x.Key.EMP_NUM,
                        ShiftCount = x.Count(),
                        MissedIn = x.Sum(y => y.Count_In_Missing),
                        MissedOut = x.Sum(y => y.Count_Out_Missing),
                        LateIn = x.Sum(y => y.Clock_In_Not_Acceptable)
                    })
                    .OrderBy(x => x.Name)
                    .ToList();
            }
        }

        public List<ClockAggregate> StoreSummary
        {
            get
            {
                return PunchDetail.GroupBy(x => new { x.CST_CNTR_int, x.StoreName })
                    .OrderBy(x => x.Key.CST_CNTR_int)
                    .Select(x =>
                    new ClockAggregate
                    {
                        Name = x.Key.CST_CNTR_int + " - " + x.Key.StoreName,
                        ShiftCount = x.Count(),
                        MissedIn = x.Sum(y => y.Count_In_Missing),
                        MissedOut = x.Sum(y => y.Count_Out_Missing),
                        LateIn = x.Sum(y => y.Clock_In_Not_Acceptable)
                    })
                    .ToList();
            }
        }

        public List<ClockAggregate> RegionSummary
        {
            get
            {
                return PunchDetail.GroupBy(x => new { x.Region, x.Division }).Select(x =>
                    new ClockAggregate
                    {
                        Name = x.Key.Region.ToString(),
                        Division = x.Key.Division,
                        ShiftCount = x.Count(),
                        MissedIn = x.Sum(y => y.Count_In_Missing),
                        MissedOut = x.Sum(y => y.Count_Out_Missing),
                        LateIn = x.Sum(y => y.Clock_In_Not_Acceptable)
                    })
                    .OrderBy(x => x.Name)
                    .ToList();
            }
        }

        public List<ClockAggregate> DivisionSummary
        {
            get
            {
                return PunchDetail.GroupBy(x => new { x.Division }).Select(x =>
                        new ClockAggregate
                        {
                            Name = x.Key.Division,
                            ShiftCount = x.Count(),
                            MissedIn = x.Sum(y => y.Count_In_Missing),
                            MissedOut = x.Sum(y => y.Count_Out_Missing),
                            LateIn = x.Sum(y => y.Clock_In_Not_Acceptable)
                        })
                    .OrderBy(x => x.Name)
                    .ToList();
            }
        }

        public ClockAggregate ChainSummary
        {
            get
            {
                return PunchDetail.GroupBy(x => new { x.CHN_CD }).Select(x =>
                    new ClockAggregate
                    {
                        Name = x.Key.CHN_CD,
                        ShiftCount = x.Count(),
                        MissedIn = x.Sum(y => y.Count_In_Missing),
                        MissedOut = x.Sum(y => y.Count_Out_Missing),
                        LateIn = x.Sum(y => y.Clock_In_Not_Acceptable)
                    })
                    .OrderBy(x => x.Name)
                    .First();
            }
        }

        public List<CPW_Clocking_DataView> RegionGMPunches => PunchDetail.Where(x => x.ROLE == "1").ToList();

        public List<ClockAggregate> RegionGMSummary
        {
            get
            {
                return RegionGMPunches.GroupBy(x => new { x.FORENAME, x.EMP_NUM, x.StoreName, x.CST_CNTR_int})
                    .OrderBy(x => x.Key.CST_CNTR_int)
                    .Select(x =>
                        new ClockAggregate
                        {
                            Name = x.Key.FORENAME,
                            empNum = x.Key.EMP_NUM,
                            StoreName = x.Key.CST_CNTR_int.ToString() + " - " + x.Key.StoreName,
                            ShiftCount = x.Count(),
                            MissedIn = x.Sum(y => y.Count_In_Missing),
                            MissedOut = x.Sum(y => y.Count_Out_Missing),
                            LateIn = x.Sum(y => y.Clock_In_Not_Acceptable)
                        })
                    .ToList();
            }
        }
    }
}