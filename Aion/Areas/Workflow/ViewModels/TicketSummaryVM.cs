using Aion.DAL.Entities;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Aion.Areas.Workflow.ViewModels
{
    public class TicketSummaryVM : BaseVm
    {
        public List<vw_TicketStubRef> TicketCollection { get; set; }
        public List<SelectListItem> _TPCMenu { get; set; }
        public List<SelectListItem> _TypeMenu { get; set; }

        public TicketSummaryVM()
        {
            _TypeMenu = new List<SelectListItem>
            {
                new SelectListItem {Value = "0", Text = "All", Selected = true }
            };
        }
    }
}