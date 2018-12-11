using System.Collections.Generic;

namespace Aion.Models.WFM
{
    public class SACheckView
    {
        public int CheckID { get; set; }
        public int QuestionID { get; set; }
        public string Text { get; set; }

        public virtual ICollection<SACheckAnswerView> SACheckAnswers { get; set; }
    }
}