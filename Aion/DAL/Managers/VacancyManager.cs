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
        public async Task<List<sp_GetRecruitmentDetail_Result>> GetVacancyDetail(string storeNum)
        {
            using(var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                return await Task.Run(() => context.sp_GetRecruitmentDetail(crit).ToList());
            }
        }

        public async Task<List<VacancyRequest>> GetPendingRequests(string storeNum)
        {
            using (var context = new VacanciesModel())
            {
                var crit = short.Parse(storeNum);
                return await context.VacancyRequests.Where(x => x.StoreNumber == crit && x.SFRefNo == null && !x.Rejected).Include("VacancyPosition").ToListAsync();
            }
        }

        public async Task<bool> PostNewRequests(List<RecruitmentRequest> requests, string notes, string storeNum, string userName)
        {
            using (var context = new VacanciesModel())
            {
                try
                {
                    var _storeNum = short.Parse(storeNum);
                    var currentAllowances = await Task.Run(() => context.sp_GetRecruitmentDetail(_storeNum).ToList());
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
                        Replace = x.Action == "replace"
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