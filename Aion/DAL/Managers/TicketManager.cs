﻿using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class TicketManager : ITicketManager
    {
        public async Task<List<sp_CheckHelpTickets_Result>> GetHelpTickets(string storeNumber)
        {
            using (var context = new WebMasterModel())
            {
                short crit = short.Parse(storeNumber);
                return await Task.Run(() => context.sp_CheckHelpTickets(crit).ToList());
            }
        }
    }
}