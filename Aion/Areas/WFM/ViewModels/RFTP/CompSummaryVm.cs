using Aion.Areas.WFM.Models.RFTP;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class CompSummaryVm : BaseVm
    {
        public List<CompSummaryView> collection { get; set; }
        
        public List<int> WeekNumbers
        {
            get
            {
                return collection.GroupBy(x => x.wk).Select(x => x.Key).OrderBy(x => x).ToList();
            }
        }

        public List<string> DivisionList
        {
            get
            {
                return collection.Where(x => x.Division != null).GroupBy(x => x.Division).Select(x => x.Key).ToList();
            }
        }

        public List<int?> BranchList
        {
            get
            {
                return collection.Where(x => x.StoreCount != null).OrderBy(x => x.StoreCount).GroupBy(x => x.StoreCount).Select(x => x.Key).ToList();
            }
        }

        public List<short?> GetRegionList(string _division)
        {
            return collection.Where(x => x.Division == _division && x.Region != null).GroupBy(x => x.Region).Select(x => x.Key).ToList();
        }

        public string selectedDate { get; set; }

        public CompSummaryVm()
        {
            WeeksOfYear = new List<SelectListItem>();
            string[] yrs = ConfigurationManager.AppSettings["FinancialYear"].ToString().Split('/');
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