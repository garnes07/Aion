using Aion.Areas.WFM.Models.MyStore;
using Aion.Areas.WFM.Models.Deployment;
using AutoMapper;
using Aion.Models.ProfitLoss;
using Aion.Models.Shared;
using Aion.Models.Vacancy;
using Aion.DAL.Entities;
using Aion.Areas.WFM.Models.RFTP;
using Aion.Areas.Workflow.Models;
using Aion.Areas.WFM.Models.FuturePlanning;
using Aion.Models.WFM;
using Aion.Models.WebMaster;

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
                cfg.AddProfile(new VacancyProfile());
                cfg.AddProfile(new WFMProfile());
                cfg.AddProfile(new WebMasterProfile());

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

    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<vw_IncorrectVacancies, IncorrectVacanciesView>();
            CreateMap<vw_OfferApprovals, OfferApprovalsView>();
            CreateMap<OfferComment, OfferCommentView>();
            CreateMap<RequestComment, RequestCommentView>();
            CreateMap<VacancyPosition, VacancyPositionView>();
            CreateMap<vw_VacancyRequestsAdmin, VacancyRequestsAdminView>();
            CreateMap<WFM_FUTURE_DATED, WFMFutureDatedView>();
            CreateMap<vw_OpenVacancySummary, OpenVacancySummaryView>();
            CreateMap<VacancyRequest, VacancyRequestsAdminView>();
            CreateMap<vw_SFOpenVacancies, SFOpenVacancyView>();
        }
    }

    public class WFMProfile : Profile
    {
        public WFMProfile()
        {
            CreateMap<WFM_EMPLOYEE_INFO, WFMEmployeeInfoView>();
            CreateMap<WFM_EMPLOYEE_INFO_UNEDITED, WFMEmployeeInfoView>();
            CreateMap<vw_4WeekSummary, SummaryView>();
            CreateMap<vw_4WeekSummary_Pilot, SummaryView>();
            CreateMap<StoreOpeningTime, StoreOpeningTimeView>();
            CreateMap<OpeningTimesComment, OpeningTimesCommentView>();
            CreateMap<AvailabilityDay, AvailabilityDayView>();
            CreateMap<AvailabilityPattern, AvailabilityPatternView>();
            CreateMap<PayCalendarDate, PayCalendarDateView>();
            CreateMap<PayCalendarRef, PayCalendarRefView>();
            CreateMap<KronosEmployeeSummary, KronosEmployeeSummaryView>();
            CreateMap<vw_CPW_Clocking_Data, CPW_Clocking_DataView>();
        }
    }

    public class WebMasterProfile : Profile
    {
        public WebMasterProfile()
        {
            CreateMap<Activity, ActivityView>();
            CreateMap<UserAccess, UserAccessView>();
            CreateMap<UserAccessArea, UserAccessAreaView>();
            CreateMap<StoreMaster, StoreMasterView>();
            CreateMap<IpRef, IpRefView>();
            CreateMap<TicketTemplate, TicketTemplateView>();
            CreateMap<TicketType, TicketTypeView>();
            CreateMap<vw_TicketStubRef, TicketStubRefView>();
            CreateMap<TicketStub, TicketStubView>();
            CreateMap<sp_EscalationOptions_Result, EscalationOptionsView>();
            CreateMap<TicketComment, TicketCommentView>();
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