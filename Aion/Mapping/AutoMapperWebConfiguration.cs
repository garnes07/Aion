using Aion.Areas.WFM.Models.MyStore;
using AutoMapper;
using Aion.DAL.Entities;
using Aion.Areas.WFM.Models.RFTP;
using Aion.Areas.Workflow.Models;

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
}