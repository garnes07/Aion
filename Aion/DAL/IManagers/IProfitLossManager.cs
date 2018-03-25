using System.Collections.Generic;
using System.Threading.Tasks;
using Aion.DAL.Entities;

namespace Aion.DAL.Managers
{
    public interface IProfitLossManager
    {
        Task<List<sp_GetPandLChannel_Result>> GetChannelPandL(string channelName);
        Task<List<sp_GetPandLChannel_Result>> GetChannelPandL(string channelName, string year, int? month);
        Task<List<sp_GetPandLChannelSummary_Result>> GetChannelPLSummary(string channelName);
        Task<List<sp_GetPandLChannelSummary_Result>> GetChannelPLSummary(string channelName, string year, int? month);
        Task<List<sp_GetPandLDivision_Result>> GetDivisionPandL(string divisionName);
        Task<List<sp_GetPandLDivision_Result>> GetDivisionPandL(string divisionName, string year, int? month);
        Task<List<sp_GetPandLDivisionSummary_Result>> GetDivisionPLSummary(string divisionName);
        Task<List<sp_GetPandLDivisionSummary_Result>> GetDivisionPLSummary(string divisionName, string year, int? month);
        Task<List<sp_GetPandLRegion_Result>> GetRegionPandL(string regionNumber);
        Task<List<sp_GetPandLRegion_Result>> GetRegionPandL(string regionNumber, string year, int? month);
        Task<List<sp_GetPandLRegionSummary_Result>> GetRegionPLSummary(string region);
        Task<List<sp_GetPandLRegionSummary_Result>> GetRegionPLSummary(string region, string year, int? month);
        Task<List<sp_GetPandL_Result>> GetStorePandL(string storeNumber);
        Task<List<sp_GetPandL_Result>> GetStorePandL(string storeNumber, string year, int? month);
    }
}