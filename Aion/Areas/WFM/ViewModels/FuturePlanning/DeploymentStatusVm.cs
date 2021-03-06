﻿using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.FuturePlanning
{
    public class DeploymentStatusVm : BaseVm
    {
        public List<DeploymentStatusView> collection { get; set; }

        public List<short?> StoreList => collection.Where(x => x.StoreNumber != null).GroupBy(x => x.StoreNumber).Select(x => x.Key).ToList();
        public List<short?> RegionList => collection.Where(x => x.Region != null).GroupBy(x => x.Region).Select(x => x.Key).ToList();
        public List<string> DivisionList => collection.Where(x => x.Division != null).GroupBy(x => x.Division).Select(x => x.Key).ToList();
        public string ChainList => collection.First().Chain;
        public List<short?> RegionsInDivision(string division)
        {
            return collection.Where(x => x.Division == division && x.Region != null).GroupBy(x => x.Region).Select(x => x.Key).ToList();
        }

        public List<int?> WeekList => collection.GroupBy(x => x.WeekNumber).Select(x => x.Key).ToList();
        
        public List<DeploymentStatusView> ChainSummary => collection.Where(x => x.Division == null).OrderBy(x => x.WeekNumber).ToList();
        public List<DeploymentStatusView> DivisionSummary(string division)
        {
            return collection.Where(x => x.Division == division && x.Region == null).OrderBy(x => x.WeekNumber).ToList();
        }
    }
}