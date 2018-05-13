using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.Areas.Admin.ViewModels.UKDashboard;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IUKDashManager
    {
        Task<bool> DeleteRoleReferenceRecord(string id);
        Task<bool> DeleteStoreReferenceRecord(int id);
        Task<DashboardErrors> GetErrors();
        bool PushData();
        Task<List<RoleReference>> RoleReferenceList();
        Task<RoleReference> RoleReferenceSingle(string id);
        bool RunBudgets();
        bool RunBuild();
        bool RunBuildNewData();
        Task<List<StoreReference>> StoreReferenceList();
        StoreReference StoreReferenceSearch(string keyword);
        Task<StoreReference> StoreReferenceSingle(int? id);
        Task<bool> SubmitNewRoleReference(RoleReference model);
        Task<bool> SubmitNewStoreReference(StoreReference model);
        Task<bool> SubmitRoleReferenceChange(RoleReference model);
        Task<bool> SubmitStoreReferenceChange(StoreReference model);
    }
}