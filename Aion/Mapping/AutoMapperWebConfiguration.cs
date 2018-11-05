using Aion.Areas.WFM.Models.MyStore;
using Aion.Areas.WFM.Models.Deployment;
using AutoMapper;
using Aion.Models.ProfitLoss;
using Aion.Models.Shared;
using Aion.DAL.Entities;
using Aion.Areas.WFM.Models.RFTP;
using Aion.Areas.Workflow.Models;
using Aion.Areas.WFM.Models.FuturePlanning;

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
                cfg.AddProfile(new ProfitLossProfile());
                cfg.AddProfile(new AdminDashProfile());
                cfg.AddProfile(new DashboardProfile());
                cfg.AddProfile(new MyStoreProfile());
                cfg.AddProfile(new TicketProfile());
                cfg.AddProfile(new AdminProfile());
            });
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

    public class AdminDashProfile : Profile
    {
        public AdminDashProfile()
        {
            CreateMap<StoreReference, StoreReferenceView>();
            CreateMap<RoleReference, RoleReferenceView>();
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
            CreateMap<vw_SOHSwings, DeploymentSwingView>();
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
            CreateMap<sp_GetRecruitmentDetailCPW_Result, RecruitmentRequest>();
            CreateMap<sp_GetRecruitmentDetailDXNS_Result, RecruitmentRequest>();
        }
    }

    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketQ_A, TicketAnswer>();
        }
    }

    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<sp_GetRecruitmentDetailCPW_Result, RecruitmentDetail>();
            CreateMap<sp_GetRecruitmentDetailDXNS_Result, RecruitmentDetail>();
        }
    }
}