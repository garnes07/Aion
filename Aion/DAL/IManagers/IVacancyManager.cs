﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;
using Aion.Areas.WFM.Models.MyStore;

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
    }
}