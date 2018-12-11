using Aion.Models.WebMaster;
using System.Collections.Generic;

namespace Aion.Areas.Workflow.ViewModels
{
    public class NewSubmissionVm
    {
        public List<TicketTemplateView> QuestionList { get; set; }
        public string FormName { get; set; }
    }
}