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
    }
}
