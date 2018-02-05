using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface ITicketManager
    {
        Task<List<sp_CheckHelpTickets_Result>> GetHelpTickets(string storeNumber);
    }
}