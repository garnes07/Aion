using Aion.DAL.Entities;
using Aion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class TOWSummaryVm : BaseVm
    {
        public List<vw_Top100CreditSummary> collection { get; set; }

        public string SundayDate
        {
            get
            {
                var selected = WeeksOfYear.First(x => x.Selected).Text;
                var SundayDate = DateTime.Parse(selected.Substring(11, 10));
                return SundayDate.ToShortDateString();
            }
        }
        public string SaturdayDate
        {
            get
            {
                var selected = WeeksOfYear.First(x => x.Selected).Text;
                var SundayDate = DateTime.Parse(selected.Substring(11, 10));
                return SundayDate.AddDays(6).ToShortDateString();              
            }
        }        
    }
}