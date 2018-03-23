using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.Models.Kronos;
using System;

namespace Aion.DAL.Managers
{
    public interface IKronosManager
    {
        Task<List<HyperFindResult>> GetKronosHyperFind(string kronosStoreName, string startDate, string endDate, string sessionId = null);
        Task<List<PunchStatus>> GetPunchStatus(List<string> empList, string sessionId = null);
        Task<List<Timesheet>> GetTimesheet(DateTime[] dates, string personNumber, string sessionID = null);
    }
}