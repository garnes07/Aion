﻿using Aion.Areas.Admin.Models;
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
                        var currentRole = currentAllowances.FirstOrDefault(x => x.PositionId == item.Position);
                        double percToAllowance;
                        if (item.Action == "replace")
                        {
                            percToAllowance = (double)(item.TotalHours + currentRole.ContractBase - currentRole.Allowance + currentRole.OpenVacancies)/ (currentRole.ContractBase);
                        }
                        else
                        {
                            percToAllowance = (double)(item.TotalHours + currentRole.ContractBase - currentRole.Allowance) / (currentRole.ContractBase);
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

        public async Task<List<vw_IncorrectVacancies>> GetIncorrectVacancies()
        {
            using(var context = new VacanciesModel())
            {
                return await context.vw_IncorrectVacancies.OrderBy(x => x.Company).ThenBy(x => x.Store_Number).ToListAsync();
            }
        }

        public async Task<List<vw_OfferApprovals>> GetOfferApprovals()
        {
            using (var context = new VacanciesModel())
            {
                return await context.vw_OfferApprovals.OrderBy(x => x.Company).ThenBy(x => x.Store_Number).ToListAsync();
            }
        }

        public async Task<List<vw_VacancyRequestsAdmin>> GetPendingForAdmin()
        {
            using (var context = new VacanciesModel())
            {
                return await context.vw_VacancyRequestsAdmin.Where(x => x.Show && x.SFRefNo == null).OrderBy(x => x.Chain).ThenBy(x => x.StoreNumber).ThenBy(x => x.PositionCode).Include("VacancyPosition").ToListAsync();
            }
        }

        public async Task<List<vw_VacancyRequestsAdmin>> GetPendingForAdmin(string Chain, int StoreNumber, int PositionCode)
        {
            using (var context = new VacanciesModel())
            {
                return await context.vw_VacancyRequestsAdmin
                    .Where(x => x.Show && x.SFRefNo == null && !x.Approved && x.Chain == Chain && x.StoreNumber == StoreNumber && x.PositionCode == PositionCode)
                    .OrderBy(x => x.Chain)
                    .ThenBy(x => x.StoreNumber)
                    .ThenBy(x => x.PositionCode)
                    .Include("VacancyPosition")
                    .Include("RequestComments")
                    .ToListAsync();
            }
        }

        public async Task<List<vw_VacancyRequestsAdmin>> GetToPostForAdmin(string Chain, int StoreNumber, int PositionCode)
        {
            using (var context = new VacanciesModel())
            {
                return await context.vw_VacancyRequestsAdmin
                    .Where(x => x.Show && x.SFRefNo == null && x.Approved && x.Chain == Chain && x.StoreNumber == StoreNumber && x.PositionCode == PositionCode)
                    .OrderBy(x => x.Chain)
                    .ThenBy(x => x.StoreNumber)
                    .ThenBy(x => x.PositionCode)
                    .Include("VacancyPosition")
                    .Include("RequestComments")
                    .ToListAsync();
            }
        }

        public async Task<bool> MarkIncorrectDone(int reqId, string username)
        {
            using(var context = new VacanciesModel())
            {
                try
                {
                    var toEdit = await context.IncorrectVacancies.FindAsync(reqId);
                    if(toEdit != null)
                    {
                        toEdit.Cancelled = true;
                        toEdit.UpdatedBy = username;
                        toEdit.DateTimeUpdated = DateTime.Now;
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<bool> MarkOfferApproved(int reqId, bool approved, string username)
        {
            using(var context = new VacanciesModel())
            {
                try
                {
                    var toEdit = await context.OfferApprovals.Where(x => x.Job_Req_Id == reqId && x.Approved == null).FirstOrDefaultAsync();
                    if(toEdit != null)
                    {
                        toEdit.Approved = approved;
                        toEdit.ApprovedBy = username;
                        toEdit.ApprovedDate = DateTime.Now;
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<RequestComment> AddNewComment(int[] reqId, string username, string commentText, string type)
        {
            using(var context = new VacanciesModel())
            {
                var now = DateTime.Now;
                foreach(var item in reqId)
                {
                    context.RequestComments.Add(new RequestComment
                    {
                        RequestId = item,
                        CommentType = type,
                        Comment = commentText,
                        EnteredOn = now,
                        EnteredBy = username
                    });
                }

                await context.SaveChangesAsync();
                return new RequestComment { RequestId = reqId[0], Comment = commentText, EnteredBy = username, EnteredOn = now, CommentType = type };
            }
        }

        public async Task<bool> AddReviewOutcome(List<ReviewOutcome> outcomes, string username)
        {
            using(var context = new VacanciesModel())
            {
                try
                {
                    var entryIdList = outcomes.Select(y => y.EntryId).ToList();
                    var existing = await context.VacancyRequests.Where(x => entryIdList.Contains(x.EntryId) && !x.Approved && !x.Rejected).Include("RequestComments").ToListAsync();

                    foreach (var item in outcomes)
                    {
                        var thisExisting = existing.FirstOrDefault(x => x.EntryId == item.EntryId);
                        if (item.ApprovalStatus == "a")
                        {
                            thisExisting.Approved = true;
                        }
                        else if (item.ApprovalStatus == "r")
                        {

                            thisExisting.Rejected = true;
                            thisExisting.Show = false;
                            thisExisting.RequestComments.Add(new RequestComment
                            {
                                CommentType = "Reject",
                                Comment = item.Note,
                                EnteredOn = DateTime.Now,
                                EnteredBy = username
                            });
                        }
                    }

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public async Task<bool> MarkAsPosted(string chain, int storenumber, int jobcode, int SFRef, string contract, string username)
        {
            using(var context = new VacanciesModel())
            {
                try
                {
                    var _chain = chain == "Travel" ? "Dixons" : chain;
                    var existing = await context.VacancyRequests.Where(x => x.Approved && x.SFRefNo == null && x.Chain == _chain && x.StoreNumber == storenumber && x.PositionCode == jobcode).ToListAsync();
                    if (existing != null)
                    {
                        foreach(var item in existing)
                        {
                            item.SFRefNo = SFRef;
                            item.PostedDate = DateTime.Now;
                            item.PostedBy = username;
                            item.ContractType = contract;
                        }
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}