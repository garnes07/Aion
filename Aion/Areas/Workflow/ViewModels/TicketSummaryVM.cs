using Aion.DAL.Entities;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Aion.Areas.Workflow.ViewModels
{
    public class TicketSummaryVM : BaseVm
    {
        public List<vw_TicketStubRef> TicketCollection { get; set; }        
        public List<SelectListItem> _TypeMenu { get; set; }
        public bool TPCView { get; set; }

        public string TPCSelected
        {
            get
            {
                return HttpContext.Current.Session["_WFTPCSelect"].ToString();
            }
        }

        public TicketSummaryVM()
        {
            _TypeMenu = new List<SelectListItem>
            {
                new SelectListItem {Value = "0", Text = "All", Selected = true }
            };
            TPCView = false;

            if(HttpContext.Current.Session["_WFTPCSelect"] == null)
            {
                HttpContext.Current.Session["_WFTPCSelect"] = "101";
            }
        }

        public List<SelectListItem> _TPCMenu
        {
            get
            {
                var toReturn = new List<SelectListItem>();

                for (int i = 1; i <= 25; i++)
                {
                    toReturn.Add(new SelectListItem { Value = (100 + i).ToString(), Text = "CPW" + i.ToString().PadLeft(2,'0') });
                }

                for (int i = 1; i <= 18; i++)
                {
                    toReturn.Add(new SelectListItem { Value = (700 + i).ToString(), Text = "DC" + i.ToString().PadLeft(2,'0') });
                }

                for (int i = 1; i <= 5; i++)
                {
                    toReturn.Add(new SelectListItem { Value = (i).ToString(), Text = "ROI " + i.ToString() });
                }

                toReturn.ForEach(x => x.Selected = x.Value == TPCSelected);
                return toReturn;
            }
        }
    }
}