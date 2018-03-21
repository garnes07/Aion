using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface ITicketManager
    {
        Task<List<TicketType>> GetTypeList();
        Task<List<UserGroup>> GetTPCList();
        int GetUserGroup(string _userName);
        Task<List<sp_CheckHelpTickets_Result>> GetHelpTickets(string storeNumber);
        Task<List<vw_TicketStubRef>> GetTicketsSelf(string userName, bool open);
        Task<List<vw_TicketStubRef>> GetTicketsTPC(string userName, bool open);
        Task<List<vw_TicketStubRef>> GetTicketsByAuth(int groupId, bool open);
        Task<TicketStub> GetSingleTicket(int ticketId, int groupId, string userName);
        int ChkInteractLvl(int level, int ticketTypeId);
        Task<TicketComment> AddNewComment(string commentText, string userName, int ticketId);
        List<sp_EscalationOptions_Result> GetEscalationOptions(int ticketType, int level);
        Task<int> CancelTicket(int ticketId, string userName);
        Task<int> CompleteTicket(int ticketId, string userName, int groupId);
        Task<int> SendTicket(int ticketId, string userName, int lvlAction, int groupId);
        Task<string> GetRegion(int storeNumber);
        Task<List<TicketTemplate>> GetFormTemplate(int _TicketTypeId);
        Task<string> GetFormName(int _TicketTypeId);
        Task<StoreMaster> GetStore(string storeNumber);
        Task<int> SubmitTicket(int TicketTypeId, string RaisedBy, string storeNumber, List<TicketAnswer> QA, int _exception);
    }
}