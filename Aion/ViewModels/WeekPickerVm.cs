using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Aion.ViewModels
{
    public class WeekPickerVm
    {
        public string Action { get; set; }
        public string Controller { get; set; }

        [Required]
        [Display(Name = "Select Date")]
        public string SelectedDate { get; set; }

        public List<SelectListItem> WeeksOfYear { get; set; }
    }
}