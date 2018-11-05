using Aion.DAL.Entities;
using Aion.Models.Shared;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class UKDashManager : IAdminDashManager
    {
        public async Task<List<string>> GetStoreErrors()
        {
            using (var context = new ROIDashModel())
            {
                return await context.fn_UnmatchedStores().ToListAsync();
            }
        }

        public async Task<List<string>> GetRoleErrors()
        {
            using (var context = new ROIDashModel())
            {
                return await context.fn_UnmatchedRoles().ToListAsync();
            }
        }

        //Run sp to build dashboard with existing timecard data file
        public bool RunBuild()
        {
            using (var context = new UKDashModel())
            {
                context.Database.ExecuteSqlCommand("reBuildDashboard");
                return true;
            }
        }

        //Run sp to build dashboard with new timecard data file
        public bool RunBuildNewData()
        {
            using (var context = new UKDashModel())
            {
                context.Database.ExecuteSqlCommand("reBuildDashboardNewData");
                return true;
            }
        }

        //Run sp to import and update budgets from file
        public bool RunBudgets()
        {
            using (var context = new UKDashModel())
            {
                context.Database.ExecuteSqlCommand("sp_LoadBudgets");
                return true;
            }
        }

        //Run sp to import and update budgets from file
        public bool PushData()
        {
            using (var context = new UKDashModel())
            {
                context.Database.ExecuteSqlCommand("sp_PushLWDataToSite");
                return true;
            }
        }

        //Get all store reference records
        public async Task<List<StoreReference>> StoreReferenceList()
        {
            using (var context = new UKDashModel())
            {
                return await context.StoreReferences.OrderBy(x => x.Br_).ToListAsync();
            }
        }

        //Get single store reference record by id
        public async Task<StoreReference> StoreReferenceSingle(int? id)
        {
            using (var context = new UKDashModel())
            {
                return await context.StoreReferences.FindAsync(id);
            }
        }

        //Get matching store reference record by branch number
        public StoreReference StoreReferenceSearch(string keyword)
        {
            using (var context = new UKDashModel())
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
        public async Task<bool> SubmitNewStoreReference(StoreReferenceView model)
        {
            using (var context = new UKDashModel())
            {
                try
                {
                    context.StoreReferences.Add(new StoreReference
                    {
                        Br_ = model.Br_,
                        UK_BR = model.UK_BR,
                        Store_Name = model.Store_Name,
                        Division = model.Division,
                        Region = model.Region,
                        Region_name = model.Region_name,
                        Channel = model.Channel
                    });
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
        public async Task<bool> SubmitStoreReferenceChange(StoreReferenceView model)
        {
            using (var context = new UKDashModel())
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
            using (var context = new UKDashModel())
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
            using (var context = new UKDashModel())
            {
                return await context.RoleReferences.OrderBy(x => x.Role).ToListAsync();
            }
        }

        //Get single role reference record by id
        public async Task<RoleReference> RoleReferenceSingle(string id)
        {
            using (var context = new UKDashModel())
            {
                return await context.RoleReferences.FindAsync(id);
            }
        }

        //Submit new role reference record
        public async Task<bool> SubmitNewRoleReference(RoleReferenceView model)
        {
            using (var context = new UKDashModel())
            {
                try
                {
                    context.RoleReferences.Add(new RoleReference
                    {
                        Role = model.Role,
                        Reporting_Role_Flag = model.Reporting_Role_Flag,
                        Sales_Role_Flag = model.Sales_Role_Flag
                    });
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
        public async Task<bool> SubmitRoleReferenceChange(RoleReferenceView model)
        {
            using (var context = new UKDashModel())
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
            using (var context = new UKDashModel())
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