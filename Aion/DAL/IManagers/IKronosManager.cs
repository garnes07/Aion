using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.Models.Kronos;

namespace Aion.DAL.Managers
{
    public interface IKronosManager
    {
        Task<List<HyperFindResult>> GetKronosHyperFind(string kronosStoreName, string startDate, string endDate, string sessionID = null);
    }
}