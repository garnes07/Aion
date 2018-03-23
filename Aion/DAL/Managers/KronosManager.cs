using Aion.DAL.Kronos;
using Aion.Models.Kronos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class KronosManager : IKronosManager
    {
        public async Task<List<HyperFindResult>> GetKronosHyperFind(string kronosStoreName, string startDate, string endDate, string sessionId = null)
        {
            var hyperFindResult = await KronosApi.HyperfindResult(kronosStoreName, string.Format("{0}-{1}", startDate, endDate), sessionId);
            return hyperFindResult;
        }

        public async Task<List<PunchStatus>> GetPunchStatus(List<string> empList, string sessionId = null)
        {
            var toRtn = new List<PunchStatus>();
            if (empList.Any())
            {
                toRtn = await KronosApi.RequestPunchStatus(empList, sessionId);
            }

            return toRtn;
        }

        public async Task<List<Timesheet>> GetTimesheet(DateTime[] dates, string personNumber, string sessionID = null)
        {
            return KronosApi.RequestTimesheet(dates, personNumber, sessionID);
        }
    }
}