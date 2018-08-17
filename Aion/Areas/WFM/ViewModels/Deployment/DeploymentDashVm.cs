using Aion.ViewModels;
using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class DeploymentDashVm : BaseVm
    {
        public List<vw_DivisionDeploymentDash> divisionCollection { get; set; }
        public List<vw_RegionDeploymentDash> regionCollection { get; set; }
        public List<vw_StoreDeploymentDash> storeCollection { get; set; }

        public List<vw_StoreDeploymentRank> storeRankCollection { get; set; }

        public string selectedDate { get; set; }
        public short? Period { get; set; }
        public int? MaxWeek { get; set; }
        public List<int> WeekNumbers { get; set; }

        public List<short> StoreList => storeCollection.GroupBy(x => x.StoreNumber).Select(x => x.Key).OrderBy(x => x).ToList();

        public DeploymentDashVm()
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

        public void setMenus(short type)
        {
            if(type == 2)
            {
                Period = storeCollection.FirstOrDefault().Period;
                MaxWeek = storeCollection.Max(x => x.WeekNumber);
                WeekNumbers = storeCollection.GroupBy(x => x.WeekNumber).Select(x => x.Key).OrderBy(x => x).ToList();
            }
            else if(type == 3)
            {
                Period = regionCollection.FirstOrDefault().Period;
                MaxWeek = regionCollection.Max(x => x.WeekNumber);
                WeekNumbers = regionCollection.GroupBy(x => x.WeekNumber).Select(x => x.Key).OrderBy(x => x).ToList();
            }
            else if(type == 4)
            {
                Period = divisionCollection.FirstOrDefault().Period;
                MaxWeek = divisionCollection.Max(x => x.WeekNumber);
                WeekNumbers = divisionCollection.GroupBy(x => x.WeekNumber).Select(x => x.Key).OrderBy(x => x).ToList();
            }
        }
    }
}