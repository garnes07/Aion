using Aion.Models.WFM;
using Aion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.RFTP
{
    public class SelfAssessmentFormVm : BaseVm
    {
        public List<SAQuestionView> Questions { get; set; }
        public bool errorPayroll { get; set; }

        private List<string> _catList;
        public List<string> CatList => _catList ?? (_catList = Questions.GroupBy(x => x.Category).Select(x => x.Key).ToList());

        public SACheckView GetRandomCheck(List<SACheckView> a)
        {
            Random rnd = new Random();
            int r = rnd.Next(a.Count);

            return a[r];
        }

    }
}