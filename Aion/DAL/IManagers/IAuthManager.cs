using Aion.DAL.Entities;
using Aion.Models.WebMaster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aion.DAL.IManagers
{
    public interface IAuthManager
    {
        Task<UserAccess> GetAccessList(string _userName, string _payroll);
        Task<int> RecordLogIn(UserLog _entry);
        Task<bool> RegisterStore(UnknownIpLog _entry);
        Task<bool> RegisterStoreFullIP(IpRef _entry);
        Task<List<UserAccess>> GetAllUsers();
        Task<bool> AddNewUserRecord(UserAccessView userDetail);
        Task<bool> DeleteUser(string username);
        Task<bool> EditUser(UserAccessView userDetail);
        Task<bool> CheckMinervaAccess(string username);
        vw_ROIMismatch CheckROIRemap(string payrollNum);
    }
}
