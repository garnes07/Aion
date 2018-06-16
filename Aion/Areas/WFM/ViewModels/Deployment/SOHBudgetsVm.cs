using System.Collections.Generic;
using System.Linq;
using Aion.Areas.WFM.Models.Deployment;
using Aion.ViewModels;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class SOHBudgetsVm : BaseVm
    {
        public List<SOHBudgetView> Collection { get; set; }

        public List<string> AdditionTypes => Collection.GroupBy(x => x.Type).Select(x => x.Key).Where(x => x != null).ToList();

        private List<int?> _WeekList;
        public List<int?> WeekList => _WeekList ?? (_WeekList = Collection.GroupBy(x => x.YearWeek).OrderBy(x => x.Key)
                                          .Select(x => x.Key).ToList());

        private List<short?> _StoreList;
        public List<short?> StoreList => _StoreList ??
                                        (_StoreList = Collection.GroupBy(x => x.StoreNumber).Select(x => x.Key)
                                            .OrderBy(x => x).ToList());

        private List<SOHBudgetView> _StoreBaseSummary;
        public List<SOHBudgetView> StoreBaseSummary => 
            _StoreBaseSummary ?? (_StoreBaseSummary = Collection
                .GroupBy(x => new {x.StoreNumber, x.StoreName, x.YearWeek})
                .Select(x => new SOHBudgetView
                {
                    StoreNumber = x.Key.StoreNumber,
                    StoreName = x.Key.StoreName,
                    YearWeek = x.Key.YearWeek,
                    OriginalTarget = x.Average(y => y.OriginalTarget)
                })
                .OrderBy(x => x.StoreNumber).ThenBy(x => x.YearWeek)
                .ToList()
            );

        private List<SOHBudgetView> _StoreAdditionsSummary;
        public List<SOHBudgetView> StoreAdditionSummary =>
            _StoreAdditionsSummary ?? (_StoreAdditionsSummary = Collection
                .GroupBy(x => new {x.StoreNumber, x.StoreName, x.YearWeek, x.Type})
                .Select(x => new SOHBudgetView
                {
                    StoreNumber = x.Key.StoreNumber,
                    StoreName = x.Key.StoreName,
                    YearWeek = x.Key.YearWeek,
                    Type = x.Key.Type,
                    Hours = x.Sum(y => y.Hours)
                })
                .OrderBy(x => x.StoreNumber).ThenBy(x => x.YearWeek)
                .ToList()
            );

        private List<SOHBudgetView> _StoreTotalSummary;
        public List<SOHBudgetView> StoreTotalSummary =>
            _StoreTotalSummary ?? (_StoreTotalSummary = Collection
                .GroupBy(x => new {x.StoreNumber, x.StoreName, x.YearWeek})
                .Select(x => new SOHBudgetView
                {
                    StoreNumber = x.Key.StoreNumber,
                    StoreName = x.Key.StoreName,
                    YearWeek = x.Key.YearWeek,
                    OriginalTarget = x.Average(y => y.OriginalTarget) + (float)x.Sum(y => y.Hours)
                })
                .OrderBy(x => x.StoreNumber).ThenBy(x => x.YearWeek)
                .ToList()
            );
    }
}