using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using System;
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

        public async Task<bool> RecordLogIn(string _userName)
        {
            using (var dbContext = new WebMasterModel())
            {
                UserLog entry = new UserLog
                {
                    UserName = _userName,
                    Timestamp = DateTime.Now
                };

                dbContext.UserLogs.Attach(entry);
                int result = await dbContext.SaveChangesAsync();

                return result > 0 ? true : false;
            }
        }
    }
}