using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;
using Aion.Areas.WFM.Models.MyStore;

namespace Aion.DAL.Managers
{
    public interface IVacancyManager
    {
        Task<List<sp_GetRecruitmentDetailCPW_Result>> GetVacancyDetailCPW(string storeNum);
        Task<List<sp_GetRecruitmentDetailDXNS_Result>> GetVacancyDetailDXNS(string storeNum);
        Task<List<VacancyRequest>> GetPendingRequests(string storeNum);
        Task<bool> PostNewRequests(List<RecruitmentRequest> requests, string notes, string storeNum, string userName);
    }
}