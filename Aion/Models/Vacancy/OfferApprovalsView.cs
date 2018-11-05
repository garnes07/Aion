using System;
using System.Collections.Generic;

namespace Aion.Models.Vacancy
{
    public class OfferApprovalsView
    {
        public long RowId { get; set; }
        public int Job_Req_Id { get; set; }
        public string Position_Number { get; set; }
        public string Colleague_Classification { get; set; }
        public string Reason_for_Vacancy { get; set; }
        public string Company { get; set; }
        public string Business_Unit { get; set; }
        public string Area { get; set; }
        public string Division { get; set; }
        public string Region { get; set; }
        public string Department { get; set; }
        public short? Store_Number { get; set; }
        public string Location { get; set; }
        public string Cost_Centre { get; set; }
        public short? Job_Code { get; set; }
        public string Job_Role { get; set; }
        public string Contract_Type { get; set; }
        public string Hiring_Manager { get; set; }
        public int Application_ID { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public decimal? Contracted_Hours { get; set; }
        public DateTime? Contract_End_Date { get; set; }
        public DateTime? Last_Modified { get; set; }
        public string Applicant_Name { get; set; }
        public bool Approved { get; set; }
        public bool Rejected { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        
        public ICollection<OfferCommentView> OfferComments { get; set; }

        public OfferApprovalsView()
        {
            OfferComments = new List<OfferCommentView>();
        }
    }
}