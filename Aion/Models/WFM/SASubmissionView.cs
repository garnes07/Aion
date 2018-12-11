using System;
using System.Collections.Generic;

namespace Aion.Models.WFM
{
    public class SASubmissionView
    {
        public int EntryId { get; set; }
        public string PersonNumber { get; set; }
        public string PersonName { get; set; }
        public string Year { get; set; }
        public byte? Period { get; set; }
        public DateTime DateTimeSubmitted { get; set; }
        public bool ActionPlan { get; set; }

        public ICollection<SASubmissionAnswerView> SASubmissionAnswers { get; set; }
    }
}