using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aion.DAL.IManagers
{
    public interface IAuthManager
    {
        Task<List<UserAccess>> GetAccessList(string _userName, string _payroll);
        Task<bool> RecordLogIn(string _userName);
    }
}
