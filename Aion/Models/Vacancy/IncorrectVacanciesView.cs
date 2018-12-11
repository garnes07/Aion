using System;

namespace Aion.Models.Vacancy
{
    public class IncorrectVacanciesView
    {
        public long RowId { get; set; }
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
        public short? Job_Code { get; set; }
        public string Job_Role { get; set; }
        public string recruitingV2_RCM_JOB_REQ_CLASSIFICATION_TYPE { get; set; }
        public string Hiring_Manager { get; set; }
        public DateTime? Date_Created { get; set; }
        public string Created_by { get; set; }
        public string Approved_Date { get; set; }
        public string Total_Vacant_Hours { get; set; }
        public string Hours_to_Display_on_Advert { get; set; }
        public double? Headcount_to_Recruit { get; set; }
        public DateTime? Last_Modified { get; set; }
        public bool? Cancelled { get; set; }
    }
}