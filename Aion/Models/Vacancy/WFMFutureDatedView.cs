using System;

namespace Aion.Models.Vacancy
{
    public class WFMFutureDatedView
    {
        public int EntryId { get; set; }
        public string Legal_Entity { get; set; }
        public string Chain { get; set; }
        public string Dept { get; set; }
        public string ID { get; set; }
        public int? Alternate_ID { get; set; }
        public string Status { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Grade { get; set; }
        public short? Job_Code { get; set; }
        public string Job_Title { get; set; }
        public decimal? Std_Hrs_Wk { get; set; }
        public double? Hrly_Rate { get; set; }
        public decimal? Annual_Rt { get; set; }
        public string Sex { get; set; }
        public DateTime? Hire_Date { get; set; }
        public DateTime? Termination_Date { get; set; }
        public string Perm_Temp { get; set; }
        public DateTime? Effective_Date_of_Change { get; set; }
        public short? NewDeptID { get; set; }
        public short? CostCentre { get; set; }
        public string EmailAddress { get; set; }
    }
}