using Aion.Areas.WFM.Models.MyStore;
using AutoMapper;
using Aion.DAL.Entities;
using Aion.Areas.WFM.Models.RFTP;
using Aion.Areas.Workflow.Models;
using Aion.Areas.ProfitLoss.Models;

namespace Aion.Mapping
{
    public class AutoMapperWebConfiguration
    {
        private static MapperConfiguration _mapperConfig;

        public static MapperConfiguration MapperConfig
        {
            get
            {
                if (_mapperConfig == null) Configure();

                return _mapperConfig;
            }
        }

        public static void Configure()
        {
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DashboardProfile());
                cfg.AddProfile(new MyStoreProfile());
                cfg.AddProfile(new TicketProfile());
                cfg.AddProfile(new ProfitLossProfile());
            });
        }
    }

    public class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            CreateMap<sp_ComplianceSummary_Result, CompSummaryView>();
            CreateMap<sp_ComplianceSummaryRegion_Result, CompSummaryView>();
            CreateMap<sp_ComplianceSummaryStore_Result, CompSummaryView>();
            CreateMap<vw_4WeekSummary_Pilot, vw_4WeekSummary>();
        }
    }

    public class MyStoreProfile : Profile
    {
        public MyStoreProfile()
        {
            CreateMap<StoreOpeningTime, NewOpeningTime>();
            CreateMap<NewOpeningTime, StoreOpeningTime>();
            CreateMap<vw_PublishedBudgetsROI, SOHBudgetView>();
            CreateMap<vw_PublishedBudgetsUK, SOHBudgetView>();
        }
    }

    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketQ_A, TicketAnswer>();
        }
    }

    public class ProfitLossProfile : Profile
    {
        public ProfitLossProfile()
        {
            CreateMap<sp_GetPandL_Result, ProfitLossView>();
            CreateMap<sp_GetPandLRegion_Result, ProfitLossView>();
            CreateMap<sp_GetPandLDivision_Result, ProfitLossView>();
            CreateMap<sp_GetPandLChannel_Result, ProfitLossView>();
            CreateMap<sp_GetPandLRegionSummary_Result, ProfitLossSummaryView>();
            CreateMap<sp_GetPandLDivisionSummary_Result, ProfitLossSummaryView>();
            CreateMap<sp_GetPandLChannelSummary_Result, ProfitLossSummaryView>();
        }
    }
}