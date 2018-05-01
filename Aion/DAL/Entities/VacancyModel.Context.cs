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
    
    public partial class VacanciesModel : DbContext
    {
        public VacanciesModel()
            : base("name=VacanciesModel")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<RequestComment> RequestComments { get; set; }
        public virtual DbSet<VacancyPosition> VacancyPositions { get; set; }
        public virtual DbSet<VacancyRequest> VacancyRequests { get; set; }
        public virtual DbSet<vw_SFOpenVacancies> vw_SFOpenVacancies { get; set; }
        public virtual DbSet<LiveVacanciesToCancel> LiveVacanciesToCancels { get; set; }
        public virtual DbSet<vw_IncorrectVacancies> vw_IncorrectVacancies { get; set; }
        public virtual DbSet<vw_OfferApprovals> vw_OfferApprovals { get; set; }
        public virtual DbSet<IncorrectVacancy> IncorrectVacancies { get; set; }
        public virtual DbSet<OfferApproval> OfferApprovals { get; set; }
    
        public virtual ObjectResult<sp_GetRecruitmentDetailCPW_Result> sp_GetRecruitmentDetailCPW(Nullable<short> storeNum)
        {
            var storeNumParameter = storeNum.HasValue ?
                new ObjectParameter("StoreNum", storeNum) :
                new ObjectParameter("StoreNum", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetRecruitmentDetailCPW_Result>("sp_GetRecruitmentDetailCPW", storeNumParameter);
        }
    
        public virtual ObjectResult<sp_GetRecruitmentDetailDXNS_Result> sp_GetRecruitmentDetailDXNS(Nullable<short> storeNum)
        {
            var storeNumParameter = storeNum.HasValue ?
                new ObjectParameter("StoreNum", storeNum) :
                new ObjectParameter("StoreNum", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetRecruitmentDetailDXNS_Result>("sp_GetRecruitmentDetailDXNS", storeNumParameter);
        }
    }
}
