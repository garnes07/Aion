using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public class RFTPTrackingManager : IRFTPTrackingManager
    {
        public async Task<List<RFTPCaseStub>> GetRFTPCaseSWAS(string store)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.RFTPCaseStubs
                    .Where(x =>
                        context.KronosEmployeeSummaries.Where(y =>
                            y.HomeBranch == crit
                        ).Any(y => y.PersonNumber == x.PersonNumber)
                        && x.Show)
                    .Include("RFTPCaseAudits")
                    .ToListAsync();
            }
        }

        public async Task<List<RFTPCaseStub>> GetRFTPCasesStore(string store)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(store);
                return await context.RFTPCaseStubs
                    .Where(x =>
                        context.KronosEmployeeSummaries.Where(y =>
                            y.Region == context.KronosEmployeeSummaries.FirstOrDefault(z => z.HomeBranch == crit).Region
                        ).Any(y => y.PersonNumber == x.PersonNumber)
                        && x.Show)
                    .Include("RFTPCaseAudits")
                    .ToListAsync();
            }
        }

        public async Task<List<RFTPCaseStub>> GetRFTPCasesRegion(string region)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.RFTPCaseStubs
                    .Where(x =>
                        context.KronosEmployeeSummaries.Where(y => y.Region == crit)
                            .Any(y => y.PersonNumber == x.PersonNumber)
                        && x.Show)
                    .Include("RFTPCaseAudits")
                    .ToListAsync();
            }
        }

        public async Task<List<RFTPCaseStub>> GetRFTPCasesDivision(string division)
        {
            using (var context = new WFMModel())
            {
                return await context.RFTPCaseStubs
                    .Where(x =>
                        context.KronosEmployeeSummaries.Where(y => y.Division == division)
                            .Any(y => y.PersonNumber == x.PersonNumber)
                        && x.Show)
                    .Include("RFTPCaseAudits")
                    .ToListAsync();
            }
        }

        public async Task<List<RFTPCaseAction>> GetRFTPCaseActions()
        {
            using (var context = new WFMModel())
            {
                return await context.RFTPCaseActions.ToListAsync();
            }
        }

        public async Task<bool> CheckCaseAuth(int caseID, string area, string areaName)
        {
            using (var context = new WFMModel())
            {
                switch (area)
                {
                    case "D":
                        return await context.RFTPCaseStubs.Where(x =>
                            context.KronosEmployeeSummaries.Where(y => y.Division == areaName)
                                .Any(y => y.PersonNumber == x.PersonNumber) && x.Show && x.CaseID == caseID).AnyAsync();
                    case "R":
                    {
                        short crit = short.Parse(areaName);
                        return await context.RFTPCaseStubs.Where(x =>
                            context.KronosEmployeeSummaries.Where(y => y.Region == crit)
                                .Any(y => y.PersonNumber == x.PersonNumber) && x.Show && x.CaseID == caseID).AnyAsync();
                    }
                    case "S":
                    {
                        short crit = short.Parse(areaName);
                        return await context.RFTPCaseStubs
                            .Where(x =>
                                context.KronosEmployeeSummaries.Where(y =>
                                    y.Region == context.KronosEmployeeSummaries
                                        .FirstOrDefault(z => z.HomeBranch == crit)
                                        .Region
                                ).Any(y => y.PersonNumber == x.PersonNumber)
                                && x.Show && x.CaseID == caseID)
                            .AnyAsync();
                    }
                    default:
                        return false;
                }
            }
        }

        public async Task<int> ConfirmCase(int caseID, string UserName)
        {
            using (var context = new WFMModel())
            {
                var result = context.RFTPCaseStubs.Find(caseID);
                if (result != null)
                {
                    result.Confirmed = true;
                    result.LastUpdated = DateTime.Now;
                    result.LastUpdatedBy = UserName;
                    return await context.SaveChangesAsync();
                }

                return -5;
            }
        }

        public async Task<int> OverrideCase(int caseId, string userName, string reason, string comment)
        {
            using (var context = new WFMModel())
            {
                var stub = context.RFTPCaseStubs.Find(caseId);

                if (stub != null)
                {
                    stub.Override = true;
                    stub.Completed = true;
                    stub.LastUpdated = DateTime.Now;
                    stub.LastUpdatedBy = userName;

                    stub.RFTPCaseAudits.Add(new RFTPCaseAudit
                    {
                        CaseID = caseId,
                        ActionType = "Override",
                        Comment = reason + " : " + comment,
                        CompletedBy = userName,
                        DateTimeCreated = DateTime.Now
                    });

                    return await context.SaveChangesAsync();
                }

                return -5;
            }
        }

        public async Task<int> ReassignCase(int caseId, string empNumber, string userName, string comment)
        {
            using (var context = new WFMModel())
            {
                var newEmployee = context.KronosEmployeeSummaries.FirstOrDefault(x => x.PersonNumber == empNumber);

                if (newEmployee != null || empNumber == "RM")
                {
                    var originalCase = context.RFTPCaseStubs.Find(caseId);
                    if (originalCase != null)
                    {
                        originalCase.Reassign = true;
                        originalCase.LastUpdated = DateTime.Now;
                        originalCase.LastUpdatedBy = userName;
                        originalCase.Completed = true;
                        originalCase.Show = false;

                        originalCase.RFTPCaseAudits.Add(new RFTPCaseAudit
                        {
                            CaseID = caseId,
                            ActionType = "Reassign",
                            Comment = "Assigned to " + empNumber + " : " + comment,
                            CompletedBy = userName,
                            DateTimeCreated = DateTime.Now
                        });

                        var result = context.sp_RFTPReassignCase(empNumber, caseId);

                        return await context.SaveChangesAsync();
                    }

                    return -5;
                }

                return -5;
            }
        }

        public async Task<int> SubmitAction(int caseId, string actionType, string comment, string username, string accessLevel)
        {
            using (var context = new WFMModel())
            {
                var ownerLevel = accessLevel == "3" ? byte.Parse(accessLevel) + 1 : (accessLevel == "2" ? (byte)4 : byte.Parse(accessLevel));
                if (context.RFTPCaseActions.Any(x => x.ActionType == actionType && x.OwnerLevel == ownerLevel))
                {
                    var originalCase = context.RFTPCaseStubs.Find(caseId);
                    if (originalCase != null)
                    {
                        originalCase.LastUpdated = DateTime.Now;
                        originalCase.LastUpdatedBy = username;

                        originalCase.RFTPCaseAudits.Add(new RFTPCaseAudit
                        {
                            CaseID = caseId,
                            ActionType = actionType,
                            Comment = comment,
                            CompletedBy = username,
                            DateTimeCreated = DateTime.Now
                        });
                        return await context.SaveChangesAsync();
                    }
                    return -5;
                }
                return -5;
            }
        }

        public async Task<List<vw_Last12MonthRFTPCases>> GetLast12MonthRFTPCasesRegion(string region)
        {
            var regionNo = int.Parse(region);
            using (var context = new WFMModel())
            {
                return await context.vw_Last12MonthRFTPCases
                    .Where(x =>
                        context.KronosEmployeeSummaries.Where(y => y.Region == regionNo).Any(y => y.PersonNumber == x.PersonNumber)
                        && x.Confirmed)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_Last12MonthRFTPCases>> GetLast12MonthRFTPCasesStore(string store)
        {
            var storeNo = int.Parse(store);
            using (var context = new WFMModel())
            {
                return await context.vw_Last12MonthRFTPCases
                    .Where(x =>
                        context.KronosEmployeeSummaries.Where(y =>
                            y.Region == context.KronosEmployeeSummaries.FirstOrDefault(z => z.HomeBranch == storeNo).Region
                        ).Any(y => y.PersonNumber == x.PersonNumber)
                        && x.Confirmed)
                    .ToListAsync();
            }
        }

        public async Task<List<vw_Last12MonthRFTPCases>> GetLast12MonthRFTPCasesSWAS(string store)
        {
            var storeNo = int.Parse(store);
            using (var context = new WFMModel())
            {
                return await context.vw_Last12MonthRFTPCases
                    .Where(x =>
                        context.KronosEmployeeSummaries.Where(y =>
                            y.HomeBranch == storeNo
                        ).Any(y => y.PersonNumber == x.PersonNumber)
                        && x.Confirmed)
                    .ToListAsync();
            }
        }

        public async Task<List<RFTPCaseStub>> GetAllCasesForPerson(string personNum)
        {
            using (var context = new WFMModel())
            {
                return await context.RFTPCaseStubs.Where(x => x.PersonNumber == personNum).Include("RFTPCaseAudits").OrderByDescending(x => x.DateTimeCreated).ToListAsync();
            }
        }

        public async Task<vw_RFTP_Notifications> GetRFTPNotifications(string username, string personNum)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_RFTP_Notifications.FirstOrDefaultAsync(x => x.Username == username || x.Username == personNum);
            }
        }
    }
}