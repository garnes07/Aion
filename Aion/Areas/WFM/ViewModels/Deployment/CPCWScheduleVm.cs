using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class CPCWScheduleVm : BaseVm
    {
        public List<SelectListItem> storeList { get; set; }
        public List<CPCWScheduleView> collection { get; set; }
        public bool storeSelected { get; set; }

        public List<string> TaskList => collection.GroupBy(x => x.TASKDESC).Select(x => x.Key).ToList();

        public void SetStoreList(List<CPCWStoreListView> s)
        {
            storeList = s.Select(x => new SelectListItem
            {
                Value = x.CST_CNTR_INT.ToString(),
                Text = string.Format("{0} {1}", x.CST_CNTR_INT, x.BRANCH)
            }).ToList();
        }
    }
}