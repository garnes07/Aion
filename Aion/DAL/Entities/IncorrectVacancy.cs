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
    
    public partial class IncorrectVacancy
    {
        public int Job_Req_ID { get; set; }
        public string Position_Number { get; set; }
        public string Colleague_Classification { get; set; }
        public string Reason_for_Vacancy { get; set; }
        public string Company { get; set; }
        public string Business_Unit { get; set; }
        public string Area { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string Store_Number { get; set; }
        public string Location { get; set; }
        public string Cost_Centre { get; set; }
        public Nullable<short> Job_Code { get; set; }
        public string Job_Role { get; set; }
        public string recruitingV2_RCM_JOB_REQ_CLASSIFICATION_TYPE { get; set; }
        public string Hiring_Manager { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public string Created_by { get; set; }
        public string Approved_Date { get; set; }
        public string Total_Vacant_Hours { get; set; }
        public string Hours_to_Display_on_Advert { get; set; }
        public Nullable<double> Headcount_to_Recruit { get; set; }
        public Nullable<System.DateTime> Last_Modified { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> DateTimeUpdated { get; set; }
    }
}
