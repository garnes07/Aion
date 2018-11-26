using Aion.DAL.Entities;
using Aion.Models.WFM;
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

        public async Task<List<vw_AvailabilityPattern>> GetPatternsForAvailableCover(string storeNum)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(storeNum);
                return await context.vw_AvailabilityPattern
                    .Where(x => context.AvailabilityStores.Where(y => y.StoreNumber == crit).Select(y => y.PersonNumber).Contains(x.PersonNumber))
                    .Include("AvailabilityDays")
                    .ToListAsync();
            }
        }

        public async Task<AvailabilityContact> GetContactDetailsPerson(string personNumber)
        {
            using (var context = new WFMModel())
            {
                return await context.AvailabilityContacts.FirstOrDefaultAsync(x => x.PersonNumber == personNumber);
            }
        }

        public async Task<bool> CheckAddRightsForUser(string personNumber, string storeNumber)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(storeNumber);
                var result = await context.KronosEmployeeSummaries.FirstOrDefaultAsync(x => x.PersonNumber == personNumber && x.HomeBranch == crit && x.Active == true);

                return result != null;
            }
        }

        public async Task<vw_TradingHoursForAvlblty> GetCurrentTradingHrs(string storeNum)
        {
            using (var context = new WFMModel())
            {
                short crit = short.Parse(storeNum);
                return await context.vw_TradingHoursForAvlblty.FirstOrDefaultAsync(x => x.StoreNumber == crit);
            }
        }

        public async Task<List<vw_AvailabilityCompletionRate>> GetCompletionRateRegion(string region)
        {
            using(var context = new WFMModel())
            {
                short crit = short.Parse(region);
                return await context.vw_AvailabilityCompletionRate.Where(x => x.Region == crit).OrderBy(x => x.StoreNumber).ToListAsync();
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

        public async Task<bool> SubmitNewPattern(AvailabilityPatternView newPattern, short[] storeList,string userName)
        {
            try
            {
                using(var context = new WFMModel())
                {
                    var existing = await context.AvailabilityPatterns.Include("AvailabilityDays").FirstOrDefaultAsync(x => x.PersonNumber == newPattern.PersonNumber && x.PatternType == 0);
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
                            existing.AvailabilityDays.Add(new AvailabilityDay { DayNum = item.DayNum, StartTime = item.StartTime, EndTime = item.EndTime});
                        }
                    }
                    else
                    {
                        newPattern.CreatedBy = userName;
                        newPattern.CreatedDate = DateTime.Now;
                        newPattern.LastConfirmedBy = userName;
                        newPattern.LastConfirmedDate = DateTime.Now;

                        context.AvailabilityPatterns.Add(new AvailabilityPattern
                        {
                            PersonNumber = newPattern.PersonNumber,
                            PersonName = newPattern.PersonName,
                            CreatedDate = DateTime.Now,
                            CreatedBy = userName,
                            LastConfirmedBy = userName,
                            LastConfirmedDate = DateTime.Now,
                            PatternType = newPattern.PatternType,
                            AvailabilityDays = newPattern.AvailabilityDays.Select(x => new AvailabilityDay { DayNum = x.DayNum, StartTime = x.StartTime, EndTime = x.EndTime}).ToList()
                        });
                    }

                    if(storeList != null)
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

        public async Task<bool> UpdateContactDetails(AvailabilityContactView ContactDetail, bool deleteRecord, string personNumber, bool confirmOnly)
        {
            using(var context = new WFMModel())
            {
                try
                {
                    bool changes = false;
                    var existingDetail = await context.AvailabilityContacts.FirstOrDefaultAsync(x => x.PersonNumber == personNumber);
                    if (existingDetail != null)
                    {
                        if (deleteRecord)
                        {
                            context.AvailabilityContacts.Remove(existingDetail);
                            changes = true;
                        }
                        else
                        {
                            if (confirmOnly)
                            {
                                existingDetail.LastUpdated = DateTime.Now;
                                changes = true;
                            }
                            else
                            {
                                existingDetail.Email = ContactDetail.Email;
                                existingDetail.Phone = ContactDetail.Phone;
                                existingDetail.LastUpdated = DateTime.Now;
                                changes = true;
                            }
                        }
                    }
                    else if (ContactDetail.Email != null || ContactDetail.Phone != null)
                    {
                        context.AvailabilityContacts.Add(new AvailabilityContact
                        {
                            PersonNumber = personNumber,
                            Phone = ContactDetail.Phone,
                            Email = ContactDetail.Email,
                            CreatedDate = DateTime.Now,
                            LastUpdated = DateTime.Now
                        });
                        changes = true;
                    }

                    if (changes)
                        await context.SaveChangesAsync();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}