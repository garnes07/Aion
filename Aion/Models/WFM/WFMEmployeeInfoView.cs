using System;

namespace Aion.Models.WFM
{
    public class WFMEmployeeInfoView
    {
        public string Legal_Entity { get; set; }
        public string Chain { get; set; }
        public string Dept { get; set; }
        public string ID { get; set; }
        public int Alternate_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Grade { get; set; }
        public short? JobCode { get; set; }
        public string Job_Title { get; set; }
        public decimal? Std_Hrs_Wk { get; set; }
        public double? Hrly_Rate { get; set; }
        public decimal? Annual_Rt { get; set; }
        public string Sex { get; set; }
        public DateTime? Hire_Date { get; set; }
        public string Perm_Temp { get; set; }
        public string Emp_Class { get; set; }
        public string Retail_Old_TCs { get; set; }
        public int? NewDeptID { get; set; }
        public short? CostCentre { get; set; }
        public string EmailAddress { get; set; }
        public decimal? AverageWorkedHrs { get; set; }
        public double? HolidayBalance { get; set; }
    }
}