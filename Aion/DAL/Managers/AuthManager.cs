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

        public async Task<List<UserAccess>> GetAllUsers()
        {
            using(var context = new WebMasterModel())
            {
                return await context.UserAccesses.Where(x => !x.Krn).ToListAsync();
            }
        }
        
        public async Task<bool> AddNewUserRecord(UserAccess userDetail)
        {
            using(var context = new WebMasterModel())
            {
                try
                {
                    context.UserAccesses.Add(userDetail);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteUser(string username)
        {
            using(var context = new WebMasterModel())
            {
                try
                {
                    var toDelete = await context.UserAccesses.FindAsync(username);
                    if(toDelete != null)
                    {
                        context.UserAccesses.Remove(toDelete);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;   
                }
            }
        }

        public async Task<bool> EditUser(UserAccess userDetail)
        {
            using(var context = new WebMasterModel())
            {
                try
                {
                    var existing = await context.UserAccesses.FindAsync(userDetail.UserName);
                    if(existing != null)
                    {
                        context.Entry(existing).CurrentValues.SetValues(userDetail);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}