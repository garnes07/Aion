using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aion.Areas.WFM.ViewModels.RFTPTracking
{
    public class RFTPManagerSummaryVm : BaseVm
    {
        public List<RFTPCaseStubView> Cases { get; set; }
        public List<RFTPCaseActionView> Actions { get; set; }
        public List<KronosEmployeeSummaryView> RegionManagers { get; set; }
        public int AccessLevel { get;}
        public bool SWAS { get; set; }

        public List<SelectListItem> ManagerDropdown => RegionManagers.OrderBy(x => x.PersonName).Select(x => new SelectListItem { Value = x.PersonNumber, Text = x.PersonName }).ToList();
        public List<short?> RegionList => RegionManagers.GroupBy(x => x.Region).Select(x => x.Key).OrderBy(x => x).ToList();

        public RFTPManagerSummaryVm(string chain)
        {
            var currentLvl = HttpContext.Current.Session["_AccessLevel"] == null ? 0 : int.Parse(HttpContext.Current.Session["_AccessLevel"].ToString());
            AccessLevel = currentLvl == 2 ? 3 : currentLvl;
            SWAS = chain == "SWAS";
        }
    }
}