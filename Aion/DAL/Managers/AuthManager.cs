using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class AuthManager : IAuthManager
    {
        public async Task<List<UserAccess>> GetAccessList(string _userName, string _payroll)
        {
            using (var dbContext = new WebMasterModel())
            {
                return await dbContext.UserAccesses.Where(x => x.UserName == _userName || x.UserName == _payroll).ToListAsync();
            }
        }

        public async Task<bool> RecordLogIn(UserLog _entry)
        {
            using (var dbContext = new WebMasterModel())
            {
                dbContext.UserLogs.Add(_entry);
                int result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }

        public async Task<bool> RegisterStore(UnknownIpLog _entry)
        {
            using (var dbContext = new WebMasterModel())
            {
                dbContext.UnknownIpLogs.Add(_entry);
                int result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }
    }
}