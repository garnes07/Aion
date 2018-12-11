using Aion.Models.Vacancy;
using System.Collections.Generic;

namespace Aion.Areas.Admin.ViewModels.Recruitment
{
    public class ToPostVm
    {
        public List<VacancyRequestsAdminView> RequestDetail { get; set; }
        public List<OpenVacancySummaryView> OpenVacancys { get; set; }
    }
}