using Aion.DAL.Entities;
using Aion.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Aion.Areas.WFM.ViewModels.MyStore
{
    public class VacancyRequestVm : BaseVm
    {
        public string StoreName { get; set; }
        public List<SelectListItem> PositionList { get; set; }
        public string RoleAllowance { get; set; }
        public List<VacancyRequest> PendingRequests{ get; set; }
        public List<sp_GetRecruitmentDetail_Result> RecruitmentDetail { get; set; }
                
        public void Populate(List<sp_GetRecruitmentDetail_Result> collection)
        {
            RecruitmentDetail = collection;

            PositionList = collection.OrderBy(x => x.SortOrder).Select(x => new SelectListItem
            {
                Value = x.PositionId.ToString(),
                Text = x.FriendlyName
            }).ToList();

            StoreName = string.Format("{0} - {1}", collection.First().StoreNumber, collection.First().StoreName);

            var _RoleAllowance = collection.Select(x => new string[]{ x.PositionId.ToString(), (x.Allowance - x.OpenVacancies).ToString(), x.OpenVacancies.ToString(), x.FriendlyName, x.RateOfPay.ToString() });
            RoleAllowance = JsonConvert.SerializeObject(_RoleAllowance);
        }
    }
}