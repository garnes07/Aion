using Aion.Areas.WFM.Models.RFTP;
using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class SelfAssessmentManager : ISelfAssessmentManager
    {
        public async Task<List<SASubmission>> GetSubmissionsPerson(string personNum)
        {
            using(var context = new WFMModel())
            {
                return await context.SASubmissions.Where(x => x.PersonNumber == personNum).ToListAsync();
            }
        }

        public async Task<List<SAQuestion>> GetQuestions()
        {
            using(var context = new WFMModel())
            {
                return await context.SAQuestions.Include(i => i.SAChecks.Select(s => s.SACheckAnswers)).OrderBy(x => x.SortOrder).ToListAsync();
            }
        }

        public async Task<vw_SelfAssessmentRequired> GetRequirementPerson(string personNum)
        {
            using(var context = new WFMModel())
            {
                return await context.vw_SelfAssessmentRequired.Where(x => x.PersonNumber == personNum).OrderByDescending(x => x.Year).ThenByDescending(x => x.Period).FirstOrDefaultAsync();
            }
        }

        public async Task<int> AddSubmission(string personNum, List<SAAnswers> a)
        {
            using(var context = new WFMModel())
            {
                try
                {
                    var toAdd = new SASubmission
                    {
                        PersonNumber = personNum,
                        PersonName = "",
                        DateTimeSubmitted = DateTime.Now,
                        ActionPlan = a.Any()
                    };

                    foreach(var item in a)
                    {
                        toAdd.SASubmissionAnswers.Add(new SASubmissionAnswer
                        {
                            QuestionId = item.Id
                        });
                    }

                    context.SASubmissions.Add(toAdd);
                    await context.SaveChangesAsync();
                    return toAdd.EntryId;
                }
                catch
                {
                    return -1;
                }
            }
        }
        
        public async Task<List<vw_ActionPlan>> GetActionPlan(int id)
        {
            using(var context = new WFMModel())
            {
                return await context.vw_ActionPlan.Where(x => x.EntryId == id).ToListAsync();
            }
        }
    }
}