using Aion.DAL.Entities;
using Aion.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.FuturePlanning
{
    public class RecruitmentStatusVm : BaseVm
    {
        public List<vw_StoreContractStatus> collection { get; set; }
        public List<vw_ContractStatusDetail> ContractDetail { get; set; }

        public string Legend
        {
            get
            {
                if(DisplayLevel <= 2)
                {
                    return JsonConvert.SerializeObject(collection.Select(x => x.StoreNumber + " - " + x.StoreName).ToList());
                }
                else
                {
                    return JsonConvert.SerializeObject(collection.Select(x => x.Region).ToList());
                }
            }
        }
        public string ContractBases => JsonConvert.SerializeObject(collection.Select(x => x.ContractBase).ToList());
        public string ContractHours => JsonConvert.SerializeObject(collection.Select(x => x.ContractHours * 100).ToList());
        public string VacancyHours => JsonConvert.SerializeObject(collection.Select(x => x.VacancyHours * 100).ToList());
    }
}