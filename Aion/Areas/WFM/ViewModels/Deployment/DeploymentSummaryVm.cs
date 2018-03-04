using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Aion.DAL.Entities;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class DeploymentSummaryVm : BaseVm
    {
        public List<sp_PeriodDeploymentSummary_Result> collection { get; set; }
        public string priority { get; set; }
        public string selectedDate { get; set; }

        public short? Period => collection.FirstOrDefault().Period;
        public int? MaxWeek => collection.Max(x => x.WeekNumber);

        public List<int?> WeekNumbers => collection.GroupBy(x => x.WeekNumber).Select(x => x.Key).OrderByDescending(x => x.HasValue).ThenBy(x => x).ToList();

        public string Chain => collection.First().Chain;
        public List<string> DivisionList => collection.Where(x => x.Division != null).GroupBy(x => x.Division).Select(x => x.Key).OrderBy(x => x).ToList();
        public List<short?> StoreList => collection.Where(x => x.StoreNumber != null).OrderBy(x => x.StoreNumber).GroupBy(x => x.StoreNumber).Select(x => x.Key).ToList();

        public List<short?> GetRegionList(string _division)
        {
            return collection.Where(x => x.Division == _division && x.Region != null).GroupBy(x => x.Region).Select(x => x.Key).OrderBy(x => x).ToList();
        }

        public List<sp_PeriodDeploymentSummary_Result> ChainSummary => collection.Where(x => x.Division == null).ToList();
        public List<sp_PeriodDeploymentSummary_Result> DivisionSummary => collection.Where(x => x.Division != null && x.Region == null).ToList();
        public List<sp_PeriodDeploymentSummary_Result> RegionSummary => collection.Where(x => x.Region != null && x.StoreNumber == null).ToList();


        public DeploymentSummaryVm()
        {
            WeeksOfYear = new List<SelectListItem>();
            string[] yrs = ConfigurationManager.AppSettings["FinancialYear"].Split('/');
            short a = short.Parse(yrs[0]);
            short b = short.Parse(yrs[1]);

            for (int j = 0; j < 2; j++)
            {
                for (int i = 1; i < 13; i++)
                {
                    WeeksOfYear.Add(new SelectListItem
                    {
                        Value = string.Format("{0}/{1}_{2}", a + j, b + j, i),
                        Text = string.Format("P{0} {1}/{2}", i, a + j, b + j)
                    });
                }
            }
        }
    }
}