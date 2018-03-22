using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aion.DAL.Entities;
using System.Data.Entity;

namespace Aion.DAL.Managers
{
    public class ActivityManager : IActivityManager
    {
        public async Task<List<Activity>> GetTopNews()
        {
            using (var context = new WebMasterModel())
            {
                return await context.Activities.Where(x => x.ActivityTypeId == 5).OrderByDescending(x => x.ActivityDate).ThenBy(x => x.PriorityTypeId).Take(2).ToListAsync();
            }
        }

        public async Task<List<Activity>> GetAllNews()
        {
            using (var context = new WebMasterModel())
            {
                return await context.Activities.Where(x => x.ActivityTypeId == 5).OrderByDescending(x => x.ActivityDate).ThenBy(x => x.PriorityTypeId).ToListAsync();
            }
        }
    }
}