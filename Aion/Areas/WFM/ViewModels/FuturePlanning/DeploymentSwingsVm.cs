using Aion.Areas.WFM.Models.FuturePlanning;
using Aion.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.FuturePlanning
{
    public class DeploymentSwingsVm : BaseVm
    {
        public List<DeploymentSwingView> collection { get; set; }

        public List<short?> StoreList => collection.Where(x => x.StoreNumber != null).GroupBy(x => x.StoreNumber).Select(x => x.Key).ToList();
        public List<short?> RegionList => collection.Where(x => x.Region != null).GroupBy(x => x.Region).Select(x => x.Key).ToList();
        public List<string> DivisionList => collection.Where(x => x.Division != null).GroupBy(x => x.Division).Select(x => x.Key).ToList();
        public string ChainList => collection.First().Chain;
        public List<short?> RegionsInDivision(string division)
        {
            return collection.Where(x => x.Division == division && x.Region != null).GroupBy(x => x.Region).Select(x => x.Key).ToList();
        }

        public List<int?> WeekList => collection.GroupBy(x => x.WeekNumber).Select(x => x.Key).ToList();

        public List<DeploymentSwingView> ChainSummary => collection.Where(x => x.Division == null).OrderBy(x => x.WeekNumber).ThenBy(x => x.sortOrder).ToList();
        public List<DeploymentSwingView> DivisionSummary(string division)
        {
            return collection.Where(x => x.Division == division && x.Region == null).OrderBy(x => x.WeekNumber).ToList();
        }

        public string JsonWeekList => JsonConvert.SerializeObject(WeekList);
    }
}