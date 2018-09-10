using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IRFTPTrackingManager
    {
        Task<List<RFTPCaseStub>> GetRFTPCasesDivision(string division);
        Task<List<RFTPCaseStub>> GetRFTPCasesRegion(string region);
        Task<List<RFTPCaseStub>> GetRFTPCasesStore(string store);
        Task<List<RFTPCaseStub>> GetRFTPCaseSWAS(string store);
        Task<List<RFTPCaseAction>> GetRFTPCaseActions();
        Task<bool> CheckCaseAuth(int CaseID, string area, string areaName);
        Task<int> ConfirmCase(int CaseID, string UserName);
        Task<int> OverrideCase(int caseId, string userName, string reason, string comment);
        Task<int> ReassignCase(int caseId, string empNumber, string userName, string comment);
        Task<int> SubmitAction(int caseId, string actionType, string comment, string username, string accessLevel);
        Task<List<vw_Last12MonthRFTPCases>> GetLast12MonthRFTPCasesRegion(string region);
        Task<List<vw_Last12MonthRFTPCases>> GetLast12MonthRFTPCasesStore(string store);
        Task<List<vw_Last12MonthRFTPCases>> GetLast12MonthRFTPCasesSWAS(string store);
        Task<List<RFTPCaseStub>> GetAllCasesForPerson(string personNum);
        Task<vw_RFTP_Notifications> GetRFTPNotifications(string username, string personNum);
    }
}