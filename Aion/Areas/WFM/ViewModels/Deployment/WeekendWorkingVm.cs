using System;
using System.Collections.Generic;
using System.Linq;
using Aion.ViewModels;
using Aion.Models.WFM;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class WeekendWorkingVm : BaseVm
    {
        public List<GmWeWorkingView> Collection { get; set; }

        private List<DateTime?> _PastDates;
        public List<DateTime?> PastDates => _PastDates ?? (_PastDates = Collection
                                                .Where(x => x.Date < DateTime.Now.AddDays(-1)).GroupBy(x => x.Date)
                                                .Select(x => x.Key).OrderBy(x => x).ToList());

        private List<DateTime?> _FutureDates;
        public List<DateTime?> FutureDates => _FutureDates ?? (_FutureDates = Collection
                                                  .Where(x => x.Date >= DateTime.Now.AddDays(-1)).GroupBy(x => x.Date)
                                                  .Select(x => x.Key).OrderBy(x => x).ToList());

        public List<short?> StoreList => Collection.Where(x => x.StoreNumber != null).GroupBy(x => x.StoreNumber).Select(x => x.Key).OrderBy(x => x).ToList();
        public List<short?> RegionList => Collection.Where(x => x.Region != null).GroupBy(x => x.Region).Select(x => x.Key).OrderBy(x => x).ToList();
        public List<string> DivisionList => Collection.Where(x => x.Division != null).GroupBy(x => x.Division).Select(x => x.Key).OrderBy(x => x).ToList();
        public string ChainSingle => Collection.First(x => x.Chain != null).Chain;

        public List<short?> RegionsInDivision(string division)
        {
            var test = Collection.Where(x => x.Region != null && x.Division == division).GroupBy(x => x.Region).Select(x => x.Key).OrderBy(x => x).ToList();
            return test;
        }

        public decimal? RegionWorking(DateTime? date)
        {
            var data = Collection.FirstOrDefault(x => x.StoreNumber == null && x.Date == date);
            return (decimal)(data.Worked + data.Holiday) / (data.Count - (data.Closed ?? 0)) * 100;
        }

        public decimal? RegionWorking(DateTime? date, short? region)
        {
            var data = Collection.FirstOrDefault(x => x.Region == region && x.Date == date);
            return (decimal)(data.Worked + data.Holiday) / (data.Count - (data.Closed ?? 0)) * 100;
        }

        public decimal? DivisionWorking(DateTime? date)
        {
            var data = Collection.FirstOrDefault(x => x.Division != null && x.Region == null && x.Date == date);
            return (decimal)(data.Worked + data.Holiday) / (data.Count - (data.Closed ?? 0)) * 100;
        }

        public decimal? DivisionWorking(DateTime? date, string division)
        {
            var data = Collection.FirstOrDefault(x => x.Division == division && x.Region == null && x.Date == date);
            return (decimal)(data.Worked + data.Holiday) / (data.Count - (data.Closed ?? 0)) * 100;
        }

        public decimal? ChainWorking(DateTime? date)
        {
            var data = Collection.FirstOrDefault(x => x.Chain != null && x.Division == null && x.Date == date);
            return (decimal)(data.Worked + data.Holiday) / (data.Count - (data.Closed ?? 0)) * 100;
        }
    }
}