using Aion.DAL.Managers;
using System;

namespace Aion.Helpers
{
    public static class ObjectHelpers
    {
        public static DateTime FirstDayOfWeek(this DateTime d)
        {
            return d.AddDays(-(int)d.DayOfWeek);
        }

        public static int GetWeekNumber(this string w)
        {
            int weekOfYr;
            if(!int.TryParse(w, out weekOfYr))
            {
                var dt = w.WeekNumberParse();
                var _weeksManager = new WeeksManager();
                weekOfYr = (int)_weeksManager.GetSingleWeek(dt);
            }
            return weekOfYr;
        }

        private static DateTime WeekNumberParse(this string w)
        {
            DateTime dt = DateTime.Now;
            if (string.IsNullOrEmpty(w)) return dt;
            switch (w.ToLower())
            {
                case "this week":
                    dt = DateTime.Now;
                    break;
                case "last week":
                    dt = DateTime.Now.AddDays(-7);
                    break;
                case "next week":
                    dt = DateTime.Now.AddDays(7);
                    break;
                case "week after":
                    dt = DateTime.Now.AddDays(14);
                    break;
                default:
                    dt = DateTime.TryParse(w, out dt) ? dt : dt;
                    break;
            }
            return dt.Date;
        }

        //public static string BuildGraphArray(List<DailyDeploymentArrayForm> rawData, string type)
        //{
        //    string result = "[";

        //    if (rawData == null)
        //    {
        //        return "";
        //    }

        //    if (type == "deployed")
        //    {
        //        foreach (var item in rawData)
        //        {
        //            result = result + item.Deployed.ToString() + ",";
        //        }
        //        result = result.TrimEnd(',') + "]";
        //    }
        //    else
        //    {
        //        foreach (var item in rawData)
        //        {
        //            result = result + item.Required.ToString() + ",";
        //        }
        //        result = result.TrimEnd(',') + "]";
        //    }

        //    return result;
        //}
    }
}