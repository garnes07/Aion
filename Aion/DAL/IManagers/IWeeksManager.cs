using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IWeeksManager
    {
        int? GetSingleWeek(DateTime dt);
        Task<List<int?>> GetMultipleWeeks(DateTime startDate, DateTime endDate);
        Task<List<Last12MonthList>> GetLast12MonthList();
    }
}