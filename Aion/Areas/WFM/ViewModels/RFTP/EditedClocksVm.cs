using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aion.DAL.Entities;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class EditedClocksVm : BaseVm
    {
        public List<EditedClock> StoreDetail { get; set; }
        public List<vw_EditedClocks> AggregateDetail { get; set; }

        public List<string> PersonList => StoreDetail.GroupBy(x => x.PersonName).Select(x => x.Key).OrderBy(x => x).ToList();

        public List<vw_EditedClocks> DivisionSummary => AggregateDetail.Where(x => x.Division != null && x.Region == null).OrderBy(x => x.Division).ToList();
        public List<vw_EditedClocks> RegionSummary => AggregateDetail.Where(x => x.Region != null && x.StoreNumber == null).OrderBy(x => x.Region).ToList();

        public List<EditedClock> PersonDetail(string _PersonName)
        {
            return StoreDetail.Where(x => x.PersonName == _PersonName).OrderBy(x => x.PunchDate).ToList();
        }
    }
}