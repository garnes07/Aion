namespace Aion.Models.WFM
{
    public class CPW_Clocking_DataView
    {
        public int CST_CNTR_int { get; set; }
        public string StoreName { get; set; }
        public string Division { get; set; }
        public string Chain { get; set; }
        public short Region { get; set; }
        public int EMP_NUM { get; set; }
        public string FORENAME { get; set; }
        public string SURNAME { get; set; }
        public string CONTRACT_HOURS { get; set; }
        public string ROLE { get; set; }
        public byte? DAY_NUM { get; set; }
        public int? FNCL_WK_NUM { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string PUNCHTIME_IN { get; set; }
        public string PUNCHTIME_OUT { get; set; }
        public string Clock_In_Early { get; set; }
        public string Clock_In_Late { get; set; }
        public string Clock_Out_Early { get; set; }
        public string Clock_Out_Late { get; set; }
        public int Clock_In_Acceptable { get; set; }
        public int Clock_In_Not_Acceptable { get; set; }
        public int Clock_Out_Not_Acceptable { get; set; }
        public int Clock_Out_Acceptable { get; set; }
        public int Count_In_Missing { get; set; }
        public int Count_Out_Missing { get; set; }
        public int Count_Schedule_Start { get; set; }
        public int Count_Schedule_End { get; set; }
        public int PunchInOveride { get; set; }
        public int PunchOutOveride { get; set; }
        public string ScheduleDate { get; set; }
        public string CHN_CD { get; set; }
    }
}