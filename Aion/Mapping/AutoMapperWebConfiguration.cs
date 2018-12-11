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
            CreateMap<vw_DivisionDeploymentDash, DivisionDeploymentDashView>();
            CreateMap<vw_RegionDeploymentDash, RegionDeploymentDashView>();
            CreateMap<vw_StoreDeploymentDash, StoreDeploymentDashView>();
            CreateMap<vw_StoreDeploymentRank, StoreDeploymentRankView>();
            CreateMap<vw_StoreDeploymentDashTrend, StoreDeploymentDashTrendView>();
            CreateMap<vw_StoreDeploymentRankTrend, StoreDeploymentRankTrendView>();
            CreateMap<sp_PeriodDeploymentSummary_Result, PeriodDeploymentSummaryView>();
            CreateMap<vw_DashboardData_v2, DashboardData_v2View>();
            CreateMap<sp_AllChainDashboardData_v2_Result, AllChainDashboardData_v2View>();
            CreateMap<DailyDeployment, DailyDeploymentView>();
            CreateMap<vw_DashboardData_v2_Pilot, DashboardData_v2View>();
            CreateMap<vw_DailyDeployment_Pilot, DailyDeploymentView>();
            CreateMap<PowerHoursProfile, PowerHoursProfileView>();
            CreateMap<vw_DailyDeployment_Top100, DailyDeploymentView>();
            CreateMap<vw_DashboardData_v2_Top100, DashboardData_v2View>();
            CreateMap<vw_Top100CreditSummary, Top100CreditSummaryView>();
            CreateMap<vw_FootfallHourly, FootfallHourlyView>();
            CreateMap<vw_ContractBaseDetail, ContractBaseDetailView>();
            CreateMap<vw_GmWeWorking, GmWeWorkingView>();
            CreateMap<vw_AvailabilityPatternMissing, AvailabilityPatternMissingView>();
            CreateMap<vw_AvailabilitySummary, AvailabilitySummaryView>();
            CreateMap<vw_TradingHoursForAvlblty, TradingHoursForAvlbltyView>();
            CreateMap<vw_AvailabilityCompletionRate, AvailabilityCompletionRateView>();
            CreateMap<AvailabilityStore, AvailabilityStoreView>();
            CreateMap<vw_HolidayPlanningStore, HolidayPlanningStoreView>();
            CreateMap<HolidayPlanningEmp, HolidayPlanningEmpView>();
            CreateMap<vw_HolidayPlanningRollup, HolidayPlanningRollupView>();
            CreateMap<vw_ScheduleData, ScheduleDataView>();
            CreateMap<vw_CPCWSchedules, CPCWScheduleView>();
            CreateMap<vw_CPCWStoreList, CPCWStoreListView>();
            CreateMap<GMPowerHour, GMPowerHourView>();
            CreateMap<PeakData, PeakDataView>();
            CreateMap<vw_StoreContractStatus, StoreContractStatusView>();
            CreateMap<vw_ContractStatusDetail, ContractStatusDetailView>();
            CreateMap<vw_DeploymentStatus, DeploymentStatusView>();
            CreateMap<vw_StoreLocations, StoreLocationsView>();
            CreateMap<EmpComplianceDetail, EmpComplianceDetailView>();
            CreateMap<EditedClock, EditedClockView>();
            CreateMap<vw_CPW_Clocking_Data_Trend, CPW_Clocking_Data_TrendView>();
            CreateMap<vw_CPW_Clocking_Repeat_Stores, CPW_Clocking_Repeat_StoresView>();
            CreateMap<vw_CPW_Clocking_Repeat_Employees, CPW_Clocking_Repeat_EmployeesView>();
            CreateMap<vw_EditedClocks, AggEditedClocksView>();
            CreateMap<SASubmission, SASubmissionView>();
            CreateMap<SASubmissionAnswer, SASubmissionAnswerView>();
            CreateMap<vw_SelfAssessmentRequired, SelfAssessmentRequiredView>();
            CreateMap<SAQuestion, SAQuestionView>();
            CreateMap<SACheck, SACheckView>();
            CreateMap<SACheckAnswer, SACheckAnswerView>();
            CreateMap<SAActionsRequired, SAActionsRequiredView>();
            CreateMap<vw_ActionPlan, ActionPlanView>();
            CreateMap<vw_Last12MonthRFTPCases, Last12MonthRFTPCasesView>();
            CreateMap<Last12MonthList, Last12MonthListView>();
            CreateMap<vw_RFTP_Notifications, RFTPNotificationsView>();
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
            CreateMap<sp_CheckHelpTickets_Result, CheckHelpTicketsView>();
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