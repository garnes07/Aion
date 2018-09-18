using Aion.DAL.Entities;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class DashboardTrendVm : BaseVm
    {
        public List<vw_StoreDeploymentDashTrend> storeCollection { get; set; }
        public List<vw_StoreDeploymentRankTrend> storeRankCollection { get; set; }

        public List<short?> StoreList => storeRankCollection.GroupBy(x => x.StoreNumber).OrderByDescending(x => x.Sum(y => y.BudgetFit + y.CustomerFit)).Select(x => x.Key).ToList();

        public List<byte> PeriodList => storeRankCollection.GroupBy(x => new { x.Year, x.Period }).OrderBy(x => x.Key.Year).ThenBy(x => x.Key.Period).Select(x => x.Key.Period).ToList();
    }
}