using Aion.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Aion.DAL.Managers
{
    public class ProfitLossManager : IProfitLossManager
    {
        //Get most recent P&L recordset for store
        public async Task<List<sp_GetPandL_Result>> GetStorePandL(string storeNumber)
        {
            using (var context = new ProfitLossModel())
            {
                var recentDetail = await context.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    short crit = short.Parse(storeNumber);
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => context.sp_GetPandL(crit, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.ToList();
                }
                return new List<sp_GetPandL_Result>();
            }
        }

        //Get single P&L recordset for store, year, month
        public async Task<List<sp_GetPandL_Result>> GetStorePandL(string storeNumber, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var context = new ProfitLossModel())
            {
                short crit = short.Parse(storeNumber);
                var data = await Task.Run(() => context.sp_GetPandL(crit, year, (short?)month, (short?)qtdStart, 1));

                return data.ToList();
            }
        }

        //Get most recent P&L recordset for region
        public async Task<List<sp_GetPandLRegion_Result>> GetRegionPandL(string regionNumber)
        {
            using (var context = new ProfitLossModel())
            {
                var recentDetail = await context.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => context.sp_GetPandLRegion(regionNumber, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.ToList();
                }
                return new List<sp_GetPandLRegion_Result>();
            }
        }

        //Get single P&L recordset for region, year, month
        public async Task<List<sp_GetPandLRegion_Result>> GetRegionPandL(string regionNumber, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var context = new ProfitLossModel())
            {
                var data = await Task.Run(() => context.sp_GetPandLRegion(regionNumber, year, (short?)month, (short?)qtdStart, 1));

                return data.ToList();
            }
        }

        //Get most recent P&L recordset for division
        public async Task<List<sp_GetPandLDivision_Result>> GetDivisionPandL(string divisionName)
        {
            using (var context = new ProfitLossModel())
            {
                var recentDetail = await context.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => context.sp_GetPandLDivision(divisionName, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.ToList();
                }
                return new List<sp_GetPandLDivision_Result>();
            }
        }

        //Get single P&L recordset for division, year, month
        public async Task<List<sp_GetPandLDivision_Result>> GetDivisionPandL(string divisionName, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var context = new ProfitLossModel())
            {
                var data = await Task.Run(() => context.sp_GetPandLDivision(divisionName, year, (short?)month, (short?)qtdStart, 1));

                return data.ToList();
            }
        }

        //Get most recent P&L recordset for channel
        public async Task<List<sp_GetPandLChannel_Result>> GetChannelPandL(string channelName)
        {
            using (var context = new ProfitLossModel())
            {
                var recentDetail = await context.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => context.sp_GetPandLChannel(channelName, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.ToList();
                }
                return new List<sp_GetPandLChannel_Result>();
            }
        }

        //Get single P&L recordset for channel, year, month
        public async Task<List<sp_GetPandLChannel_Result>> GetChannelPandL(string channelName, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var context = new ProfitLossModel())
            {
                var data = await Task.Run(() => context.sp_GetPandLChannel(channelName, year, (short?)month, (short?)qtdStart, 1));

                return data.ToList();
            }
        }

        //Get P&L Summary channel
        public async Task<List<sp_GetPandLChannelSummary_Result>> GetChannelPLSummary(string channelName, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var context = new ProfitLossModel())
            {
                var data = await Task.Run(() => context.sp_GetPandLChannelSummary(channelName, year, (short?)month, (short?)qtdStart, 1));

                return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
            }
        }

        //Get most recent P&L Summary channel
        public async Task<List<sp_GetPandLChannelSummary_Result>> GetChannelPLSummary(string channelName)
        {
            using (var dbContext = new ProfitLossModel())
            {
                var recentDetail = await dbContext.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => dbContext.sp_GetPandLChannelSummary(channelName, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
                }
                return new List<sp_GetPandLChannelSummary_Result>();
            }
        }

        //Get P&L Summary division
        public async Task<List<sp_GetPandLDivisionSummary_Result>> GetDivisionPLSummary(string divisionName, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var context = new ProfitLossModel())
            {
                var data = await Task.Run(() => context.sp_GetPandLDivisionSummary(divisionName, year, (short?)month, (short?)qtdStart, 1));

                return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
            }
        }

        //Get most recent P&L Summary division
        public async Task<List<sp_GetPandLDivisionSummary_Result>> GetDivisionPLSummary(string divisionName)
        {
            using (var context = new ProfitLossModel())
            {
                var recentDetail = await context.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => context.sp_GetPandLDivisionSummary(divisionName, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
                }
                return new List<sp_GetPandLDivisionSummary_Result>();
            }
        }

        //Get P&L Summary channel
        public async Task<List<sp_GetPandLRegionSummary_Result>> GetRegionPLSummary(string region, string year, int? month)
        {
            decimal mth = (decimal)month;
            var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

            using (var context = new ProfitLossModel())
            {
                var data = await Task.Run(() => context.sp_GetPandLRegionSummary(region, year, (short?)month, (short?)qtdStart, 1));

                return data.OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
            }
        }

        //Get most recent P&L Summary channel
        public async Task<List<sp_GetPandLRegionSummary_Result>> GetRegionPLSummary(string region)
        {
            using (var context = new ProfitLossModel())
            {

                var recentDetail = await context.fn_LatestPLRecord().SingleAsync();
                if (recentDetail.MaxMonth != null)
                {
                    decimal mth = (decimal)recentDetail.MaxMonth;
                    var qtdStart = (int)Math.Ceiling(mth / 3) * 3 - 2;

                    var data = await Task.Run(() => context.sp_GetPandLRegionSummary(region, recentDetail.MaxYear, recentDetail.MaxMonth, (short?)qtdStart, 1));
                    return data.ToList().OrderBy(x => x.Heirarchy).ThenBy(x => x.DetailName).ToList();
                }
                return new List<sp_GetPandLRegionSummary_Result>();
            }
        }
    }
}