//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aion.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class WFM_EMPLOYEE_INFO
    {
        public string Legal_Entity { get; set; }
        public string Chain { get; set; }
        public string Dept { get; set; }
        public string ID { get; set; }
        public int Alternate_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Grade { get; set; }
        public Nullable<short> JobCode { get; set; }
        public string Job_Title { get; set; }
        public Nullable<decimal> Std_Hrs_Wk { get; set; }
        public Nullable<double> Hrly_Rate { get; set; }
        public Nullable<decimal> Annual_Rt { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> Hire_Date { get; set; }
        public string Perm_Temp { get; set; }
        public string Emp_Class { get; set; }
        public string Retail_Old_TCs { get; set; }
        public Nullable<short> NewDeptID { get; set; }
        public Nullable<short> CostCentre { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<decimal> AverageWorkedHrs { get; set; }
        public Nullable<double> HolidayBalance { get; set; }
    }
}