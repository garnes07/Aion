using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class AvlbltyManager : IAvlbltyManager
    {
        public async Task<List<vw_AvailabilityPattern>> GetAllPatternsStore(string storeNum)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(storeNum);
                return await context.vw_AvailabilityPattern.Where(x => x.HomeBranch == crit).Include("AvailabilityDays").ToListAsync();
            }
        }

        public async Task<List<vw_AvailabilityPattern>> GetAllPatternsPerson(string personNumber)
        {
            using (var context = new WFMModel())
            {
                return await context.vw_AvailabilityPattern.Where(x => x.PersonNumber == personNumber).Include("AvailabilityDays").ToListAsync();
            }
        }

        public async Task<List<AvailabilityStore>> GetAllPatternStoresPerson(string personNumber)
        {
            using(var context = new WFMModel())
            {
                return await context.AvailabilityStores.Where(x => x.PersonNumber == personNumber).ToListAsync();
            }
        }

        public async Task<List<vw_AvailabilityPatternMissing>> GetAllColleaguesWithoutPattern(string storeNum)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(storeNum);
                return await context.vw_AvailabilityPatternMissing.Where(x => x.HomeBranch == crit).ToListAsync();
            }
        }

        public async Task<List<vw_AvailabilitySummary>> GetPatternStore(string storeNum)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(storeNum);
                return await context.vw_AvailabilitySummary.Where(x => x.HomeBranch == crit).ToListAsync();
            }
        }

        public async Task<bool> SubmitNewPattern(AvailabilityPattern newPattern, short[] storeList,string userName)
        {
            try
            {
                using(var context = new WFMModel())
                {
                    var existing = await context.AvailabilityPatterns.FirstOrDefaultAsync(x => x.PersonNumber == newPattern.PersonNumber && x.PatternType == 0);
                    var zeroTime = new TimeSpan(0, 0, 0);

                    foreach (var item in newPattern.AvailabilityDays.ToList())
                    {
                        if (item.StartTime == zeroTime)
                            newPattern.AvailabilityDays.Remove(item);
                    }

                    if (existing != null)
                    {
                        existing.LastConfirmedDate = DateTime.Now;
                        existing.LastConfirmedBy = userName;

                        foreach(var item in existing.AvailabilityDays.Where(x => !newPattern.AvailabilityDays.Any(y => y.DayNum == x.DayNum)).ToList())
                        {
                            existing.AvailabilityDays.Remove(item);
                            context.AvailabilityDays.Remove(item);
                        }

                        foreach(var item in existing.AvailabilityDays)
                        {
                            item.StartTime = newPattern.AvailabilityDays.FirstOrDefault(x => x.DayNum == item.DayNum).StartTime;
                            item.EndTime = newPattern.AvailabilityDays.FirstOrDefault(x => x.DayNum == item.DayNum).EndTime;
                        }

                        foreach(var item in newPattern.AvailabilityDays.Where(x => !existing.AvailabilityDays.Any(y => y.DayNum == x.DayNum)).ToList())
                        {
                            existing.AvailabilityDays.Add(item);
                        }
                    }
                    else
                    {
                        newPattern.CreatedBy = userName;
                        newPattern.CreatedDate = DateTime.Now;
                        newPattern.LastConfirmedBy = userName;
                        newPattern.LastConfirmedDate = DateTime.Now;

                        context.AvailabilityPatterns.Add(newPattern);
                    }

                    if(storeList.Length > 0)
                    {
                        var existingStores = await context.AvailabilityStores.Where(x => x.PersonNumber == newPattern.PersonNumber).ToListAsync();

                        foreach(var item in existingStores.Where(x => !storeList.Any(y => y == x.StoreNumber)).ToList())
                        {
                            context.AvailabilityStores.Remove(item);
                        }

                        for (int i = 0; i < storeList.Length; i++)
                        {
                            if(!existingStores.Any(x => x.StoreNumber == storeList[i]))
                            {
                                context.AvailabilityStores.Add(new AvailabilityStore
                                {
                                    PersonNumber = newPattern.PersonNumber,
                                    StoreNumber = storeList[i]
                                });
                            }                            
                        }
                    }

                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}