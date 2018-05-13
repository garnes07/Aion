using Aion.Areas.Admin.ViewModels.UKDashboard;
using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class ROIDashManager : IAdminDashManager
    {
        public async Task<DashboardErrors> GetErrors()
        {
            using (var context = new ROIDashModel())
            {
                DashboardErrors toReturn = new DashboardErrors();
                toReturn.StoreErrors = await context.fn_UnmatchedStores().ToListAsync();
                toReturn.RoleErrors = await context.fn_UnmatchedRoles().ToListAsync();
                return toReturn;
            }
        }

        //Run sp to build dashboard with existing timecard data file
        public bool RunBuild()
        {
            using (var context = new ROIDashModel())
            {
                context.Database.ExecuteSqlCommand("reBuildDashboard");
                return true;
            }
        }

        //Run sp to build dashboard with new timecard data file
        public bool RunBuildNewData()
        {
            using (var context = new ROIDashModel())
            {
                context.Database.ExecuteSqlCommand("reBuildDashboardNewData");
                return true;
            }
        }

        //Run sp to import and update budgets from file
        public bool RunBudgets()
        {
            using (var context = new ROIDashModel())
            {
                context.Database.ExecuteSqlCommand("sp_LoadBudgets");
                return true;
            }
        }

        //Run sp to import and update budgets from file
        public bool PushData()
        {
            using (var context = new ROIDashModel())
            {
                context.Database.ExecuteSqlCommand("sp_PushLWDataToSite");
                return true;
            }
        }

        //Get all store reference records
        public async Task<List<StoreReference>> StoreReferenceList()
        {
            using (var context = new ROIDashModel())
            {
                return await context.StoreReferences.OrderBy(x => x.Br_).ToListAsync();
            }
        }

        //Get single store reference record by id
        public async Task<StoreReference> StoreReferenceSingle(int? id)
        {
            using (var context = new ROIDashModel())
            {
                return await context.StoreReferences.FindAsync(id);
            }
        }

        //Get matching store reference record by branch number
        public StoreReference StoreReferenceSearch(string keyword)
        {
            using (var context = new ROIDashModel())
            {
                var criteria = 0;
                var numberSearch = int.TryParse(keyword, out criteria);
                if (numberSearch)
                {
                    return context.StoreReferences.Where(x => x.Br_ == criteria).SingleOrDefault();
                }
                return new StoreReference();
            }
        }

        //Submit new store reference record
        public async Task<bool> SubmitNewStoreReference(StoreReference model)
        {
            using (var context = new ROIDashModel())
            {
                try
                {
                    context.StoreReferences.Add(model);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }                
            }
        }

        //Submit change to store reference record
        public async Task<bool> SubmitStoreReferenceChange(StoreReference model)
        {
            using (var context = new ROIDashModel())
            {
                try
                {
                    var existing = await context.StoreReferences.FindAsync(model.Br_);
                    if (existing != null)
                    {
                        context.Entry(existing).CurrentValues.SetValues(model);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        //Delete store reference record on confirm
        public async Task<bool> DeleteStoreReferenceRecord(int id)
        {
            using (var context = new ROIDashModel())
            {
                try
                {
                    var toDelete = await context.StoreReferences.FindAsync(id);
                    if (toDelete != null)
                    {
                        context.StoreReferences.Remove(toDelete);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        //Get all role reference records
        public async Task<List<RoleReference>> RoleReferenceList()
        {
            using (var context = new ROIDashModel())
            {
                return await context.RoleReferences.OrderBy(x => x.Role).ToListAsync();
            }
        }

        //Get single role reference record by id
        public async Task<RoleReference> RoleReferenceSingle(string id)
        {
            using (var context = new ROIDashModel())
            {
                return await context.RoleReferences.FindAsync(id);
            }
        }

        //Submit new role reference record
        public async Task<bool> SubmitNewRoleReference(RoleReference model)
        {
            using (var context = new ROIDashModel())
            {
                try
                {
                    context.RoleReferences.Add(model);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }                
            }
        }
        //Submit change to role reference record
        public async Task<bool> SubmitRoleReferenceChange(RoleReference model)
        {
            using (var context = new ROIDashModel())
            {
                try
                {
                    var existing = await context.RoleReferences.FindAsync(model.Role);
                    if (existing != null)
                    {
                        context.Entry(existing).CurrentValues.SetValues(model);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        //Delete role reference record on confirm
        public async Task<bool> DeleteRoleReferenceRecord(string id)
        {
            using (var context = new ROIDashModel())
            {
                try
                {
                    var toDelete = await context.RoleReferences.FindAsync(id);
                    if (toDelete != null)
                    {
                        context.RoleReferences.Remove(toDelete);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}