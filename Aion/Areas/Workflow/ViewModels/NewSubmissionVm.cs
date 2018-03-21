using Aion.DAL.Entities;
using System.Collections.Generic;

namespace Aion.Areas.Workflow.ViewModels
{
    public class NewSubmissionVm
    {
        public List<TicketTemplate> QuestionList { get; set; }
        public string FormName { get; set; }
    }
}