using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IActivityManager
    {
        Task<List<Activity>> GetTopNews();
        Task<List<Activity>> GetAllNews();
    }
}