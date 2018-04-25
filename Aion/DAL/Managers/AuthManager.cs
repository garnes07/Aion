using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class AuthManager : IAuthManager
    {
        public async Task<UserAccess> GetAccessList(string _userName, string _payroll)
        {
            using (var context = new WebMasterModel())
            {
                var username = _userName;
                var payroll = _payroll;
                var result = await context.UserAccesses.Where(x => x.UserName == username || x.UserName == payroll).OrderByDescending(x => x.AccessLevel).Include("UserAccessAreas").FirstOrDefaultAsync();
                return result;
            }
        }

        public async Task<int> RecordLogIn(UserLog _entry)
        {
            using (var context = new WebMasterModel())
            {
                context.UserLogs.Add(_entry);
                int result = await context.SaveChangesAsync();

                var loginRecords = await context.UserLogs.Where(x => x.UserName == _entry.UserName).ToListAsync();
                return loginRecords.Count;
            }
        }

        public async Task<bool> RegisterStore(UnknownIpLog _entry)
        {
            using (var context = new WebMasterModel())
            {
                context.UnknownIpLogs.Add(_entry);
                int result = await context.SaveChangesAsync();

                return result > 0;
            }
        }

        public async Task<bool> RegisterStoreFullIP(IpRef _entry)
        {
            using (var context = new WebMasterModel())
            {
                context.IpRefs.Add(_entry);
                int result = await context.SaveChangesAsync();

                return result > 0;
            }
        }
    }
}