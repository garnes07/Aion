using Aion.Areas.WFM.Models.MyStore;
using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class VacancyManager : IVacancyManager
    {
        public async Task<List<sp_GetRecruitmentDetailCPW_Result>> GetVacancyDetailCPW(string storeNum)
        {
            using(var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                return await Task.Run(() => context.sp_GetRecruitmentDetailCPW(crit).ToList());
            }
        }

        public async Task<List<sp_GetRecruitmentDetailDXNS_Result>> GetVacancyDetailDXNS(string storeNum)
        {
            using (var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                return await Task.Run(() => context.sp_GetRecruitmentDetailDXNS(crit).ToList());
            }
        }

        public async Task<List<VacancyRequest>> GetPendingRequestsCPW(string storeNum)
        {
            using (var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                return await context.VacancyRequests.Where(x => x.StoreNumber == crit && x.Show && !x.Rejected && x.Chain == "CPW").Include("VacancyPosition").ToListAsync();
            }
        }

        public async Task<List<VacancyRequest>> GetPendingRequestsDXNS(string storeNum)
        {
            using (var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                return await context.VacancyRequests.Where(x => x.StoreNumber == crit && x.Show && !x.Rejected && x.Chain == "Dixons").Include("VacancyPosition").ToListAsync();
            }
        }

        public async Task<List<vw_SFOpenVacancies>> GetOpenVacanciesCPW(string storeNum)
        {
            using(var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                return await context.vw_SFOpenVacancies.Where(x => x.StoreNumber == crit && x.Chain == "CPW").ToListAsync();
            }
        }

        public async Task<List<vw_SFOpenVacancies>> GetOpenVacanciesDXNS(string storeNum)
        {
            using (var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                return await context.vw_SFOpenVacancies.Where(x => x.StoreNumber == crit && x.Chain == "Dixons").ToListAsync();
            }
        }

        public async Task<bool> CancelPending(string storeNum, int refId)
        {
            using(var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                var result = await context.VacancyRequests.FirstOrDefaultAsync(x => x.StoreNumber == crit && x.EntryId == refId);
                if(result != null)
                {
                    result.ToCancel = true;
                    result.Show = false;
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> CancelLive(string storeNum, int refId, string userName)
        {
            using(var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                var result = await context.vw_SFOpenVacancies.FirstOrDefaultAsync(x => x.StoreNumber == crit && x.JobReqId == refId);
                if(result != null)
                {
                    var toAdd = new LiveVacanciesToCancel
                    {
                        JobReqId = refId,
                        RaisedBy = userName,
                        RaisedDate = DateTime.Now,
                        Completed = false
                    };

                    context.LiveVacanciesToCancels.Add(toAdd);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> PostNewRequestsCPW(List<RecruitmentRequest> requests, string notes, string storeNum, string userName)
        {
            using (var context = new VacanciesModel())
            {
                try
                {
                    var _storeNum = short.Parse(storeNum);
                    var currentAllowances = await Task.Run(() => context.sp_GetRecruitmentDetailCPW(_storeNum).ToList());
                    var roleTotals = requests.GroupBy(x => new { x.Position, x.Action }).Select(x => new { x.Key.Position, x.Key.Action, TotalHours = x.Sum(y => y.Heads) * x.Sum(y => y.ContractHours) }).ToList();

                    bool valid = true;
                    foreach (var item in roleTotals)
                    {
                        double percToAllowance;
                        if (item.Action == "replace")
                        {
                            percToAllowance = item.TotalHours / (double)(currentAllowances.FirstOrDefault(x => x.PositionId == item.Position).Allowance + currentAllowances.FirstOrDefault(x => x.PositionId == item.Position).OpenVacancies);
                        }
                        else
                        {
                            percToAllowance = item.TotalHours / (double)(currentAllowances.FirstOrDefault(x => x.PositionId == item.Position).Allowance + currentAllowances.FirstOrDefault(x => x.PositionId == item.Position).OpenVacancies);
                        }

                        if (percToAllowance <= 1)
                        {
                            requests.FirstOrDefault(x => x.Position == item.Position).Approval = "Approved";
                        }
                        else if (percToAllowance <= 1.3)
                        {
                            requests.FirstOrDefault(x => x.Position == item.Position).Approval = "Review";
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                    }

                    List<VacancyRequest> requestsToAdd = requests.Select(x => new VacancyRequest
                    {
                        StoreNumber = _storeNum,
                        PositionCode = (short)x.Position,
                        Heads = (short)x.Heads,
                        ContractHrs = (short)x.ContractHours,
                        Approved = x.Approval == "Approved",
                        Rejected = false,
                        RaisedBy = userName,
                        RaisedDate = DateTime.Now,
                        Repost = x.Repost,
                        Replace = x.Action == "replace",
                        Chain = "CPW",
                        Show = true
                    }).ToList();

                    if (notes != "")
                    {
                        foreach (var item in requestsToAdd)
                        {
                            item.RequestComments.Add(new RequestComment { CommentType = "Notes", Comment = notes, EnteredOn = DateTime.Now, EnteredBy = userName });
                        }
                    }

                    foreach (var item in requestsToAdd)
                    {
                        context.VacancyRequests.Add(item);
                    }

                    await context.SaveChangesAsync();

                    return valid;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}