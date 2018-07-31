using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;
using Aion.Areas.WFM.Models.RFTP;

namespace Aion.DAL.Managers
{
    public interface ISelfAssessmentManager
    {
        Task<List<SAQuestion>> GetQuestions();
        Task<List<SASubmission>> GetSubmissionsPerson(string personNum);
        Task<vw_SelfAssessmentRequired> GetRequirementPerson(string personNum);
        Task<int> AddSubmission(string personNum, List<SAAnswers> a);
        Task<List<vw_ActionPlan>> GetActionPlan(int id);
    }
}