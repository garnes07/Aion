using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Aion.ViewModels;
using Newtonsoft.Json;
using Aion.Models.WFM;

namespace Aion.Areas.WFM.ViewModels.Deployment
{
    public class FootfallVm : BaseVm
    {
        public List<FootfallHourlyView> FootfallCollection { get; set; }
        public string SelectedWeek { get; set; }
        public string SelectedYear { get; set; }
        public short? MaxHour => FootfallCollection.Max(x => x.Hour_In_Day_24);

        //Drop down list for years
        public List<SelectListItem> Years
        {
            get
            {
                var ls = new List<SelectListItem>();
                for (var i = 0; i < 4; i++)
                {
                    var yr = 2016 + i;
                    var yrTxt = (yr - 2000) + "/" + (yr - 1999);
                    ls.Add(new SelectListItem { Value = yrTxt, Text = yrTxt });
                }
                ls.ForEach(x => x.Selected = x.Value == SelectedYear);
                return ls;
            }
        }

        //Drop down list for weeks
        public List<SelectListItem> Weeks
        {
            get
            {
                var ls = new List<SelectListItem>();
                for (var i = 1; i <= 52; i++)
                {
                    ls.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ls.ForEach(x => x.Selected = x.Value == SelectedWeek);
                return ls;
            }
        }

        

        public string DailyGraphData
        {
            get
            {
                var dailyFF = FootfallCollection.GroupBy(x => x.Day).Select(x => new {x.Key, Footfall = x.Sum(y => y.Footfall_Volume)}).ToList();
                var data = dailyFF.SelectMany(x => new[]
                {
                    dailyFF.SingleOrDefault(y => y.Key == "Sunday")?.Footfall,
                    dailyFF.SingleOrDefault(y => y.Key == "Monday")?.Footfall,
                    dailyFF.SingleOrDefault(y => y.Key == "Tuesday")?.Footfall,
                    dailyFF.SingleOrDefault(y => y.Key == "Wednesday")?.Footfall,
                    dailyFF.SingleOrDefault(y => y.Key == "Thursday")?.Footfall,
                    dailyFF.SingleOrDefault(y => y.Key == "Friday")?.Footfall,
                    dailyFF.SingleOrDefault(y => y.Key == "Saturday")?.Footfall
                });
                return JsonConvert.SerializeObject(data);
            }
        }

        public string HourlyGraphData(string day)
        {
            var dayData = FootfallCollection.Where(x => x.Day == day).GroupBy(x => x.Hour_In_Day_24).Select(x => new {x.Key, Footfall = x.Sum(y => y.Footfall_Volume)}).ToList();

            List<int> data = new List<int>();

            for (int i = 8; i <= FootfallCollection.Max(x => x.Hour_In_Day_24); i++)
            {
                data.Add(dayData.Where(x => x.Key == i).Select(x => x.Footfall).FirstOrDefault() ?? 0);
            }

            return JsonConvert.SerializeObject(data);
        }
    }
}
