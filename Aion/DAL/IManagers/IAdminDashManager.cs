using Aion.DAL.Entities;
using Aion.Models.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public interface IAdminDashManager
    {
        Task<bool> DeleteRoleReferenceRecord(string id);
        Task<bool> DeleteStoreReferenceRecord(int id);
        Task<List<string>> GetStoreErrors();
        Task<List<string>> GetRoleErrors();
        bool PushData();
        Task<List<RoleReference>> RoleReferenceList();
        Task<RoleReference> RoleReferenceSingle(string id);
        bool RunBudgets();
        bool RunBuild();
        bool RunBuildNewData();
        Task<List<StoreReference>> StoreReferenceList();
        StoreReference StoreReferenceSearch(string keyword);
        Task<StoreReference> StoreReferenceSingle(int? id);
        Task<bool> SubmitNewRoleReference(RoleReferenceView model);
        Task<bool> SubmitNewStoreReference(StoreReferenceView model);
        Task<bool> SubmitRoleReferenceChange(RoleReferenceView model);
        Task<bool> SubmitStoreReferenceChange(StoreReferenceView model);
    }
}