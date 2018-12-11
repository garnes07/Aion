using System.Collections.Generic;

namespace Aion.Models.WFM
{
    public class SAQuestionView
    {
        public int QuestionID { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public short SortOrder { get; set; }
        
        public virtual ICollection<SACheckView> SAChecks { get; set; }
        public virtual ICollection<SAActionsRequiredView> SAActionsRequireds { get; set; }
        public virtual ICollection<SASubmissionAnswerView> SASubmissionAnswers { get; set; }
    }
}