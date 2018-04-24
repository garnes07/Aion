using Aion.DAL.Entities;
using System.Threading.Tasks;

namespace Aion.DAL.IManagers
{
    public interface IAuthManager
    {
        Task<UserAccess> GetAccessList(string _userName, string _payroll);
        Task<int> RecordLogIn(UserLog _entry);
        Task<bool> RegisterStore(UnknownIpLog _entry);
        Task<bool> RegisterStoreFullIP(IpRef _entry);
    }
}
