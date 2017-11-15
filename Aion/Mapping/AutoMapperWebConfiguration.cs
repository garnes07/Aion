using AutoMapper;

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

            });
        }
    }

    //public class DashboardProfile : Profile
    //{
    //    public DashboardProfile()
    //    {
    //        CreateMap<sp_RegionDashboardData_Result, DashboardView>();
    //        CreateMap<sp_DivisionDashboardData_Result, DashboardView>();
    //        CreateMap<sp_ChannelDashboardData_Result, DashboardView>();
    //        CreateMap<sp_AllDivisionDashboardData_Result, DashboardView>();
    //        CreateMap<sp_AllChannelDashboardData_Result, DashboardView>();
    //        CreateMap<DashBoardData, DashboardView>();
    //        CreateMap<sp_RegionPunchCompliance_Result, RegionPunchCompItem>();
    //    }
    //}
}