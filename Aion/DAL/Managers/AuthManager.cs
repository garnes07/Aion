﻿using Aion.DAL.Entities;
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
            using (var context = new WebMasterModel())
            {
                return await context.UserAccesses.Where(x => x.UserName == _userName || x.UserName == _payroll).ToListAsync();
            }
        }

        public async Task<bool> RecordLogIn(UserLog _entry)
        {
            using (var context = new WebMasterModel())
            {
                context.UserLogs.Add(_entry);
                int result = await context.SaveChangesAsync();

                return result > 0;
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