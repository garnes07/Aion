using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Aion.ViewModels
{
    public class BaseVm
    {
        public string Message { get; set; }

        public short DisplayLevel { get; set; } = -1;

        public MessageType MessageType { get; set; }

        public List<SelectListItem> WeeksOfYear;
        public string SelectedDate { get; set; }

        public void SetWeeksOfYear(DateTime lastDate, List<int?> weekNumbers)
        {
            var ls = new List<SelectListItem>();
            var _date = lastDate;

            foreach(var week in weekNumbers)
            {
                ls.Add(new SelectListItem { Value = week.ToString(), Text = string.Format("{0} (wc {1})", week.ToString(), _date.ToShortDateString())});
                _date = _date.AddDays(-7);
            }
            WeeksOfYear = ls;
        }

        public string GetAlertType
        {
            get
            {
                var toRtn = "alert-info";
                switch (MessageType)
                {
                    case MessageType.Info:
                        toRtn = "alert-success";
                        break;
                    case MessageType.Warning:
                        toRtn = "alert-warning";
                        break;
                    case MessageType.Error:
                        toRtn = "alert-danger";
                        break;
                }

                return toRtn;
            }
        }
    }

    public enum MessageType
    {
        Info,
        Warning,
        Error
    }
}
