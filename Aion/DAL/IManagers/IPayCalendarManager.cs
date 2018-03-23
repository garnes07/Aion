using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IPayCalendarManager
    {
        Task<List<PayCalendarDate>> GetPayCalendarDates(string chain, string period);
        Task<List<PayCalendarRef>> GetPayCalendarRef(string chain);
    }
}