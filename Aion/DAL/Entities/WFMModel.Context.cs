﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aion.DAL.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WFMModel : DbContext
    {
        public WFMModel()
            : base("name=WFMModel")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmpComplianceDetail> EmpComplianceDetails { get; set; }
        public virtual DbSet<KronosEmployeeSummary> KronosEmployeeSummaries { get; set; }
        public virtual DbSet<vw_CPW_Clocking_Data> vw_CPW_Clocking_Data { get; set; }
        public virtual DbSet<vw_DashboardData_v2> vw_DashboardData_v2 { get; set; }
        public virtual DbSet<vw_CPW_Clocking_Data_Trend> vw_CPW_Clocking_Data_Trend { get; set; }
        public virtual DbSet<vw_CPW_Clocking_Repeat_Employees> vw_CPW_Clocking_Repeat_Employees { get; set; }
        public virtual DbSet<vw_CPW_Clocking_Repeat_Stores> vw_CPW_Clocking_Repeat_Stores { get; set; }
        public virtual DbSet<EditedClock> EditedClocks { get; set; }
        public virtual DbSet<vw_EditedClocks> vw_EditedClocks { get; set; }
        public virtual DbSet<RFTPCaseAudit> RFTPCaseAudits { get; set; }
        public virtual DbSet<RFTPCaseStub> RFTPCaseStubs { get; set; }
        public virtual DbSet<RFTPCaseAction> RFTPCaseActions { get; set; }
        public virtual DbSet<vw_Last12MonthRFTPCases> vw_Last12MonthRFTPCases { get; set; }
        public virtual DbSet<DailyDeployment> DailyDeployments { get; set; }
        public virtual DbSet<vw_FootfallHourly> vw_FootfallHourly { get; set; }
        public virtual DbSet<vw_GmWeWorking> vw_GmWeWorking { get; set; }
        public virtual DbSet<StoreOpeningTime> StoreOpeningTimes { get; set; }
        public virtual DbSet<vw_PublishedBudgetsROI> vw_PublishedBudgetsROI { get; set; }
        public virtual DbSet<vw_PublishedBudgetsUK> vw_PublishedBudgetsUK { get; set; }
        public virtual DbSet<HrFeed> HrFeeds { get; set; }
        public virtual DbSet<vw_ContractBaseDetail> vw_ContractBaseDetail { get; set; }
        public virtual DbSet<vw_ScheduleData> vw_ScheduleData { get; set; }
        public virtual DbSet<HolidayPlanningEmp> HolidayPlanningEmps { get; set; }
        public virtual DbSet<vw_HolidayPlanningStore> vw_HolidayPlanningStore { get; set; }
        public virtual DbSet<vw_HolidayPlanningRollup> vw_HolidayPlanningRollup { get; set; }
        public virtual DbSet<vw_4WeekSummary> vw_4WeekSummary { get; set; }
        public virtual DbSet<vw_StoreContractStatus> vw_StoreContractStatus { get; set; }
        public virtual DbSet<vw_DeploymentStatus> vw_DeploymentStatus { get; set; }
        public virtual DbSet<PayCalendarDate> PayCalendarDates { get; set; }
        public virtual DbSet<PayCalendarRef> PayCalendarRefs { get; set; }
        public virtual DbSet<PowerHoursProfile> PowerHoursProfiles { get; set; }
        public virtual DbSet<vw_DailyDeployment_Pilot> vw_DailyDeployment_Pilot { get; set; }
        public virtual DbSet<vw_DashboardData_v2_Pilot> vw_DashboardData_v2_Pilot { get; set; }
        public virtual DbSet<vw_4WeekSummary_Pilot> vw_4WeekSummary_Pilot { get; set; }
        public virtual DbSet<OpeningTimesComment> OpeningTimesComments { get; set; }
        public virtual DbSet<AvailabilityDay> AvailabilityDays { get; set; }
        public virtual DbSet<AvailabilityPattern> AvailabilityPatterns { get; set; }
        public virtual DbSet<vw_AvailabilityPattern> vw_AvailabilityPattern { get; set; }
        public virtual DbSet<vw_AvailabilityPatternMissing> vw_AvailabilityPatternMissing { get; set; }
        public virtual DbSet<vw_AvailabilitySummary> vw_AvailabilitySummary { get; set; }
        public virtual DbSet<vw_PilotDailyUnders> vw_PilotDailyUnders { get; set; }
        public virtual DbSet<AvailabilityStore> AvailabilityStores { get; set; }
        public virtual DbSet<vw_TradingHoursForAvlblty> vw_TradingHoursForAvlblty { get; set; }
        public virtual DbSet<vw_AvailabilityCompletionRate> vw_AvailabilityCompletionRate { get; set; }
        public virtual DbSet<AvailabilityContact> AvailabilityContacts { get; set; }
        public virtual DbSet<vw_StoreLocations> vw_StoreLocations { get; set; }
        public virtual DbSet<vw_ROIMismatch> vw_ROIMismatch { get; set; }
        public virtual DbSet<vw_SOHSwings> vw_SOHSwings { get; set; }
        public virtual DbSet<Top100Ref> Top100Ref { get; set; }
        public virtual DbSet<vw_DailyDeployment_Top100> vw_DailyDeployment_Top100 { get; set; }
        public virtual DbSet<vw_DashboardData_v2_Top100> vw_DashboardData_v2_Top100 { get; set; }
        public virtual DbSet<vw_Top100CreditSummary> vw_Top100CreditSummary { get; set; }
        public virtual DbSet<SACheckAnswer> SACheckAnswers { get; set; }
        public virtual DbSet<SACheck> SAChecks { get; set; }
        public virtual DbSet<SAQuestion> SAQuestions { get; set; }
        public virtual DbSet<SASubmission> SASubmissions { get; set; }
        public virtual DbSet<vw_SelfAssessmentRequired> vw_SelfAssessmentRequired { get; set; }
        public virtual DbSet<SAActionsRequired> SAActionsRequireds { get; set; }
        public virtual DbSet<SASubmissionAnswer> SASubmissionAnswers { get; set; }
        public virtual DbSet<vw_ActionPlan> vw_ActionPlan { get; set; }
        public virtual DbSet<vw_RFTP_Notifications> vw_RFTP_Notifications { get; set; }
        public virtual DbSet<vw_CPCWSchedules> vw_CPCWSchedules { get; set; }
        public virtual DbSet<vw_CPCWStoreList> vw_CPCWStoreList { get; set; }
        public virtual DbSet<vw_DivisionDeploymentDash> vw_DivisionDeploymentDash { get; set; }
        public virtual DbSet<vw_RegionDeploymentDash> vw_RegionDeploymentDash { get; set; }
        public virtual DbSet<WFM_EMPLOYEE_INFO> WFM_EMPLOYEE_INFO { get; set; }
        public virtual DbSet<vw_StoreDeploymentDash> vw_StoreDeploymentDash { get; set; }
        public virtual DbSet<vw_StoreDeploymentRank> vw_StoreDeploymentRank { get; set; }
    
        public virtual ObjectResult<sp_ComplianceSummary_Result> sp_ComplianceSummary(string chain, Nullable<int> period, string year)
        {
            var chainParameter = chain != null ?
                new ObjectParameter("Chain", chain) :
                new ObjectParameter("Chain", typeof(string));
    
            var periodParameter = period.HasValue ?
                new ObjectParameter("Period", period) :
                new ObjectParameter("Period", typeof(int));
    
            var yearParameter = year != null ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ComplianceSummary_Result>("sp_ComplianceSummary", chainParameter, periodParameter, yearParameter);
        }
    
        public virtual ObjectResult<sp_ComplianceSummaryRegion_Result> sp_ComplianceSummaryRegion(string region, Nullable<int> period, string year)
        {
            var regionParameter = region != null ?
                new ObjectParameter("Region", region) :
                new ObjectParameter("Region", typeof(string));
    
            var periodParameter = period.HasValue ?
                new ObjectParameter("Period", period) :
                new ObjectParameter("Period", typeof(int));
    
            var yearParameter = year != null ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ComplianceSummaryRegion_Result>("sp_ComplianceSummaryRegion", regionParameter, periodParameter, yearParameter);
        }
    
        public virtual ObjectResult<sp_ComplianceSummaryStore_Result> sp_ComplianceSummaryStore(string store, Nullable<int> period, string year)
        {
            var storeParameter = store != null ?
                new ObjectParameter("Store", store) :
                new ObjectParameter("Store", typeof(string));
    
            var periodParameter = period.HasValue ?
                new ObjectParameter("Period", period) :
                new ObjectParameter("Period", typeof(int));
    
            var yearParameter = year != null ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ComplianceSummaryStore_Result>("sp_ComplianceSummaryStore", storeParameter, periodParameter, yearParameter);
        }
    
        public virtual ObjectResult<sp_ComplianceSummary_Result> sp_ComplianceSummaryDivision(string division, Nullable<int> period, string year)
        {
            var divisionParameter = division != null ?
                new ObjectParameter("Division", division) :
                new ObjectParameter("Division", typeof(string));
    
            var periodParameter = period.HasValue ?
                new ObjectParameter("Period", period) :
                new ObjectParameter("Period", typeof(int));
    
            var yearParameter = year != null ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ComplianceSummary_Result>("sp_ComplianceSummaryDivision", divisionParameter, periodParameter, yearParameter);
        }
    
        public virtual ObjectResult<sp_PeriodDeploymentSummary_Result> sp_PeriodDeploymentSummary(Nullable<byte> level, string area, string year, Nullable<byte> period)
        {
            var levelParameter = level.HasValue ?
                new ObjectParameter("Level", level) :
                new ObjectParameter("Level", typeof(byte));
    
            var areaParameter = area != null ?
                new ObjectParameter("Area", area) :
                new ObjectParameter("Area", typeof(string));
    
            var yearParameter = year != null ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(string));
    
            var periodParameter = period.HasValue ?
                new ObjectParameter("Period", period) :
                new ObjectParameter("Period", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_PeriodDeploymentSummary_Result>("sp_PeriodDeploymentSummary", levelParameter, areaParameter, yearParameter, periodParameter);
        }
    
        public virtual ObjectResult<sp_AllChainDashboardData_v2_Result> sp_AllChainDashboardData_v2(string chain, Nullable<int> beginWeek)
        {
            var chainParameter = chain != null ?
                new ObjectParameter("Chain", chain) :
                new ObjectParameter("Chain", typeof(string));
    
            var beginWeekParameter = beginWeek.HasValue ?
                new ObjectParameter("BeginWeek", beginWeek) :
                new ObjectParameter("BeginWeek", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AllChainDashboardData_v2_Result>("sp_AllChainDashboardData_v2", chainParameter, beginWeekParameter);
        }
    
        public virtual ObjectResult<sp_AllChainDashboardData_v2_Result> sp_AllDivisionDashboardData_v2(string division, Nullable<int> beginWeek)
        {
            var divisionParameter = division != null ?
                new ObjectParameter("Division", division) :
                new ObjectParameter("Division", typeof(string));
    
            var beginWeekParameter = beginWeek.HasValue ?
                new ObjectParameter("BeginWeek", beginWeek) :
                new ObjectParameter("BeginWeek", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AllChainDashboardData_v2_Result>("sp_AllDivisionDashboardData_v2", divisionParameter, beginWeekParameter);
        }
    
        public virtual int sp_RFTPReassignCase(string personNumber, Nullable<int> oldCaseID)
        {
            var personNumberParameter = personNumber != null ?
                new ObjectParameter("personNumber", personNumber) :
                new ObjectParameter("personNumber", typeof(string));
    
            var oldCaseIDParameter = oldCaseID.HasValue ?
                new ObjectParameter("oldCaseID", oldCaseID) :
                new ObjectParameter("oldCaseID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_RFTPReassignCase", personNumberParameter, oldCaseIDParameter);
        }
    }
}
