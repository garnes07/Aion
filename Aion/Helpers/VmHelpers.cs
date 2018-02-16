using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Aion.Helpers
{
    public class VmHelpers
    {
        public static List<SelectListItem> GetFinancialWeeks(DateTime lastDate, List<int?> weekNumbers)
        {
            var ls = new List<SelectListItem>();
            var _date = lastDate;

            for (int i = 0; i < weekNumbers.Count(); i++)
            {
                ls.Add(new SelectListItem { Value = weekNumbers[i].ToString(), Text = string.Format("{0} (wc {1})", weekNumbers[i].ToString(), _date.ToShortDateString()) });
                _date = _date.AddDays(-7);
            }

            return ls;
        }

        public static decimal? CalcPerc(decimal? actual, decimal? budget)
        {
            if (actual.GetValueOrDefault() == 0 || budget.GetValueOrDefault() == 0) return 0;
            if (budget < 0 && actual > 0)
            {
                return (actual / Math.Abs((decimal)budget) + 2) * 100;
            }

            return actual / budget * 100;
        }

        public static double? CalcPerc(double? actual, double? budget)
        {
            if (actual.GetValueOrDefault() == 0 || budget.GetValueOrDefault() == 0) return 0;
            if (budget < 0)
            {
                return (actual / Math.Abs((double)budget) + 2) * 100;
            }

            return actual / budget * 100;
        }

        // Populate select list for opening times
        public static IEnumerable<SelectListItem> GetTimes()
        {
            TimeSpan TimeCounter = new TimeSpan(7, 0, 0);
            TimeSpan TimeIncrement = new TimeSpan(0, 15, 0);
            TimeSpan MaxTime = new TimeSpan(23, 15, 0);

            var times = new List<SelectListItem> {new SelectListItem {Text = "Closed", Value = "00:00:00"}};

            while (TimeCounter < MaxTime)
            {
                times.Add(new SelectListItem { Text = TimeCounter.ToString("hh\\:mm"), Value = TimeCounter.ToString() });
                TimeCounter = TimeCounter.Add(TimeIncrement);
            }

            return new SelectList(times, "Value", "Text");
        }
    }
}