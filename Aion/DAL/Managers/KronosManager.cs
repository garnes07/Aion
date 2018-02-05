using Aion.DAL.Kronos;
using Aion.Models.Kronos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class KronosManager : IKronosManager
    {
        public async Task<List<HyperFindResult>> GetKronosHyperFind(string kronosStoreName, string startDate, string endDate, string sessionID = null)
        {
            var hyperFindResult = await KronosApi.HyperfindResult(kronosStoreName, string.Format("{0}-{1}", startDate, endDate), sessionID);
            return hyperFindResult;
        }
    }
}