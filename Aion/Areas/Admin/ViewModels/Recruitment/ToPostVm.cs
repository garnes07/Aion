using Aion.DAL.Entities;
using System.Collections.Generic;

namespace Aion.Areas.Admin.ViewModels.Recruitment
{
    public class ToPostVm
    {
        public List<vw_VacancyRequestsAdmin> RequestDetail { get; set; }
        public List<vw_OpenVacancySummary> OpenVacancys { get; set; }
    }
}