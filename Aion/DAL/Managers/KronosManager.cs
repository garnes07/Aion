using Aion.DAL.Entities;
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

        public async Task<List<HyperFindResult>> GetKronosHyperFind(List<StoreMaster> kronosStoreNames, string startDate, string endDate, string sessionId = null)
        {
            List<HyperFindResult> result = new List<HyperFindResult>();
            string dateSpan = string.Format("{0}-{1}", startDate, endDate);

            foreach (var store in kronosStoreNames)
            {
                var kronosResult = await KronosApi.HyperfindResult(store.KronosName, dateSpan, sessionId);
                foreach(var item in kronosResult)
                {
                    item.storeNumber = store.StoreNumber;
                }
                result.AddRange(kronosResult);
            }

            return result;
        }

        public async Task<List<HyperFindResult>> GetKronosHyperFindBatch(List<StoreMaster> kronosStoreNames, string startDate, string endDate, string sessionId = null)
        {
            string dateSpan = string.Format("{0}-{1}", startDate, endDate);

            var result = await KronosApi.HyperfindResultBatch(kronosStoreNames, dateSpan, sessionId);

            return result;
        }
    }
}