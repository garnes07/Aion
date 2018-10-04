using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class TicketManager : ITicketManager
    {
        public async Task<List<TicketType>> GetTypeList()
        {
            using (var context = new WebMasterModel())
            {
#if DEBUG
                return await context.TicketTypes.Where(x => !x.Title.StartsWith("Help")).ToListAsync();
#else
                return await context.TicketTypes.Where(x => (bool)x.Live && !x.Title.StartsWith("Help")).ToListAsync();
#endif
            }
        }

        public async Task<List<UserGroup>> GetTPCList()
        {
            using (var context = new WebMasterModel())
            {
                return await context.UserGroups.Where(x => x.GroupId == 3).OrderBy(x => x.FriendlyName).ToListAsync();
            }
        }

        public int GetUserGroup(string _userName)
        {
            using (var context = new WebMasterModel())
            {
                return context.UserGroups.Where(x => x.UserName == _userName).Select(x => x.GroupId).FirstOrDefault();
            }
        }

        public async Task<List<sp_CheckHelpTickets_Result>> GetHelpTickets(string storeNumber)
        {
            using (var context = new WebMasterModel())
            {
                short crit = short.Parse(storeNumber);
                return await Task.Run(() => context.sp_CheckHelpTickets(crit).ToList());
            }
        }

        public async Task<List<vw_TicketStubRef>> GetTicketsSelf(string userName, bool open)
        {
            using (var context = new WebMasterModel())
            {
                return open ?
                    await context.vw_TicketStubRef.Where(x => x.RaisedBy == userName && x.Status == "Open").ToListAsync() :
                    await context.vw_TicketStubRef.Where(x => x.RaisedBy == userName && x.Status != "Open").ToListAsync();
            }
        }

        public async Task<List<vw_TicketStubRef>> GetTicketsTPC(string userName, bool open, int region = 101)
        {
            using (var context = new WebMasterModel())
            {
                return open ?
                    await context.vw_TicketStubRef.Where(x => x.GroupId == 3 && x.Status == "Open" && x.Region == region).ToListAsync():
                    await context.vw_TicketStubRef.Where(x => x.GroupId == 3 && x.Status != "Open" && x.Region == region).ToListAsync();
            }
        }

        public async Task<List<vw_TicketStubRef>> GetTicketsByAuth(int groupId, bool open)
        {
            using (var context = new WebMasterModel())
            {
                return open ?
                    await context.vw_TicketStubRef.Where(x => x.GroupId == groupId && x.Status == "Open").ToListAsync() :
                    await context.vw_TicketStubRef.Where(x => x.GroupId == groupId && x.Status != "Open").ToListAsync();
            }
        }

        public async Task<TicketStub> GetSingleTicket(int ticketId, int groupId, string userName)
        {
            using (var context = new WebMasterModel())
            {
                if (context.sp_CheckAccessRight(groupId, ticketId, userName).First().Value == 1)
                {
                    return await context.TicketStubs.Where(x => x.TicketId == ticketId)
                    .Include("TicketType")
                    .Include("TicketAnswers.TicketTemplate")
                    .Include("TicketComments")
                    .SingleOrDefaultAsync();
                }

                return new TicketStub();
            }
        }

        public int ChkInteractLvl(int level, int ticketTypeId)
        {
            using (var context = new WebMasterModel())
            {
                return (int)context.sp_ChkInteractLvl(level, ticketTypeId).FirstOrDefault();
            }
        }

        public async Task<TicketComment> AddNewComment(string commentText, string userName, int ticketId)
        {
            using (var context = new WebMasterModel())
            {
                TicketComment _toAttach = new TicketComment
                {
                    TicketId = ticketId,
                    User = userName,
                    Timestamp = DateTime.Now,
                    Comment = commentText
                };

                context.TicketComments.Add(_toAttach);
                await context.SaveChangesAsync();

                return _toAttach;
            }
        }

        public List<sp_EscalationOptions_Result> GetEscalationOptions(int ticketType, int level)
        {
            using (var context = new WebMasterModel())
            {
                return context.sp_EscalationOptions(ticketType, level).OrderBy(x => x.Level).ToList();
            }
        }

        public async Task<int> CancelTicket(int ticketId, string userName)
        {
            using (var context = new WebMasterModel())
            {
                var result = context.TicketStubs.Find(ticketId);
                if (result.RaisedBy != userName)
                {
                    return -5; //Error code if user does not own ticket
                }
                result.Status = "Cancelled";
                result.LastUpdate = DateTime.Now;
                result.LastUpdatedBy = userName;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> CompleteTicket(int ticketId, string userName, int groupId)
        {
            using (var context = new WebMasterModel())
            {
                if (context.sp_CheckAccessRight(groupId, ticketId, "").First().Value == 1)
                {
                    var result = context.TicketStubs.Find(ticketId);

                    result.Status = "Completed";
                    result.LastUpdate = DateTime.Now;
                    result.LastUpdatedBy = userName;

                    return await context.SaveChangesAsync();
                }
                return -5;
            }
        }

        public async Task<int> SendTicket(int ticketId, string userName, int lvlAction, int groupId)
        {
            using (var context = new WebMasterModel())
            {
                if (context.sp_CheckAccessRight(groupId, ticketId, "").First().Value == 1)
                {
                    var result = context.TicketStubs.Find(ticketId);

                    result.LastUpdate = DateTime.Now;
                    result.LastUpdatedBy = userName;

                    int toMove = result.EscalationLevel;

                    result.EscalationLevel = (byte)(toMove + lvlAction);

                    return await context.SaveChangesAsync();
                }
                return -5;
            }
        }

        public async Task<string> GetRegion(int storeNumber)
        {
            using (var context = new WebMasterModel())
            {
                var result = await context.StoreMasters.FirstOrDefaultAsync(x => x.StoreNumber == storeNumber);
                return result.Region.ToString();
            }
        }

        public async Task<StoreMaster> GetStore(string storeNumber)
        {
            using (var context = new WebMasterModel())
            {
                short crit = short.Parse(storeNumber);
                return await context.StoreMasters.FirstOrDefaultAsync(x => x.StoreNumber == crit);
            }
        }

        public async Task<List<TicketTemplate>> GetFormTemplate(int _TicketTypeId)
        {
            using (var context = new WebMasterModel())
            {
#if DEBUG
                return await context.TicketTemplates.Where(x => x.TicketTypeId == _TicketTypeId).OrderBy(x => x.QuestionId).ToListAsync();
#else
                return await context.TicketTemplates.Where(x => x.TicketTypeId == _TicketTypeId && (bool)x.TicketType.Live).OrderBy(x => x.QuestionId).ToListAsync();
#endif
            }
        }

        public async Task<string> GetFormName(int _TicketTypeId)
        {
            using (var context = new WebMasterModel())
            {
                return await context.TicketTypes.Where(x => x.TicketTypeId == _TicketTypeId).Select(x => x.Title).FirstOrDefaultAsync();
            }
        }

        public async Task<int> SubmitTicket(int TicketTypeId, string RaisedBy, string storeNumber, List<TicketAnswer> QA, int _exception)
        {
            using (var context = new WebMasterModel())
            {
                TicketStub _ticketStub = new TicketStub
                {
                    DateTimeCreated = DateTime.Now,
                    Status = "Open",
                    TicketTypeId = TicketTypeId,
                    LastUpdate = DateTime.Now,
                    RaisedBy = RaisedBy,
                    EscalationLevel = 1,
                    BranchNumber = short.Parse(storeNumber),
                    Exception = _exception
                };

                foreach (var item in QA)
                {
                    _ticketStub.TicketAnswers.Add(item);
                }

                context.TicketStubs.Add(_ticketStub);
                context.SaveChanges();

                return _ticketStub.TicketId;
            }
        }
    }
}