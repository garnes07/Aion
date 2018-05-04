using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;
using Aion.Areas.WFM.Models.MyStore;
using Aion.Areas.Admin.Models;

namespace Aion.DAL.Managers
{
    public interface IVacancyManager
    {
        Task<List<sp_GetRecruitmentDetailCPW_Result>> GetVacancyDetailCPW(string storeNum);
        Task<List<sp_GetRecruitmentDetailDXNS_Result>> GetVacancyDetailDXNS(string storeNum);
        Task<List<VacancyRequest>> GetPendingRequestsCPW(string storeNum);
        Task<List<VacancyRequest>> GetPendingRequestsDXNS(string storeNum);
        Task<List<vw_SFOpenVacancies>> GetOpenVacanciesCPW(string storeNum);
        Task<List<vw_SFOpenVacancies>> GetOpenVacanciesDXNS(string storeNum);
        Task<bool> CancelPending(string storeNum, int refId);
        Task<bool> CancelLive(string storeNum, int refId, string userName);
        Task<bool> PostNewRequestsCPW(List<RecruitmentRequest> requests, string notes, string storeNum, string userName);
        Task<List<vw_IncorrectVacancies>> GetIncorrectVacancies();
        Task<List<vw_OfferApprovals>> GetOfferApprovals();
        Task<List<vw_VacancyRequestsAdmin>> GetPendingForAdmin();
        Task<List<vw_VacancyRequestsAdmin>> GetPendingForAdmin(string Chain, int StoreNumber, int PositionCode);
        Task<List<vw_VacancyRequestsAdmin>> GetToPostForAdmin(string Chain, int StoreNumber, int PositionCode);
        Task<bool> MarkIncorrectDone(int reqId, string username);
        Task<bool> MarkOfferApproved(int reqId, bool approved, string username);
        Task<RequestComment> AddNewComment(int[] reqId, string username, string commentText, string type);
        Task<bool> AddReviewOutcome(List<ReviewOutcome> outcomes, string username);
        Task<bool> MarkAsPosted(string chain, int storenumber, int jobcode, int SFRef, string contract, string username);
    }
}