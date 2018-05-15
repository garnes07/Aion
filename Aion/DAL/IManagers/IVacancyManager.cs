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
        Task<List<VacancyRequest>> GetHistoricVacancies(int storeNum);
        Task<List<VacancyRequest>> GetPendingRequestsCPW(string storeNum);
        Task<List<VacancyRequest>> GetPendingRequestsDXNS(string storeNum);
        Task<List<vw_SFOpenVacancies>> GetOpenVacanciesCPW(string storeNum);
        Task<List<vw_SFOpenVacancies>> GetOpenVacanciesDXNS(string storeNum);
        Task<vw_SFOpenVacancies> GetOpenVacancyByRef(int JobReqID);
        Task<bool> CancelPending(string storeNum, int refId);
        Task<bool> CancelLive(string storeNum, int refId, string userName);
        Task<bool> PostNewRequestsCPW(List<RecruitmentRequest> requests, string notes, string storeNum, string userName);
        Task<List<vw_IncorrectVacancies>> GetIncorrectVacancies();
        Task<List<vw_OfferApprovals>> GetOfferApprovals();
        Task<List<vw_VacancyRequestsAdmin>> GetPendingForAdmin();
        Task<List<vw_VacancyRequestsAdmin>> GetPendingForAdmin(string Chain, int StoreNumber, int PositionCode);
        Task<List<vw_VacancyRequestsAdmin>> GetToPostForAdmin(string Chain, int StoreNumber, int PositionCode);
        Task<bool> MarkIncorrectDone(int reqId, string username);
        Task<RequestComment> AddNewComment(int[] reqId, string username, string commentText, string type);
        Task<OfferComment> AddNewOfferComment(int[] reqId, string username, string commentText, string type);
        Task<bool> AddReviewOutcome(List<ReviewOutcome> outcomes, string username);
        Task<bool> ReviewOnHold(string Chain, int StoreNumber, int PositionCode);
        Task<bool> MarkAsPosted(string chain, int storenumber, int jobcode, int SFRef, string contract, string username);
        Task<bool> HoldToPost(string chain, int storenumber, int jobcode);
        Task<bool> UnapproveToPost(string chain, int storenumber, int jobcode);
        Task<List<WFM_EMPLOYEE_INFO_UNEDITED>> GetHrCurrent(string chain, int storenumber);
        Task<List<WFM_FUTURE_DATED>> GetHrChanges(string chain, int storenumber);
        Task<List<vw_OfferApprovals>> GetOfferToReview(int JobReqId);
        Task<bool> OfferOnHold(int JobReqId);
        Task<bool> AddOfferOutcome(List<ReviewOutcome> outcomes, string username);
        Task<bool> RejectedToReview(int entryId);
    }
}