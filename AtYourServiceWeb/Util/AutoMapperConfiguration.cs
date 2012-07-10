

namespace AtYourService.Web.Util
{
    using AutoMapper;
    using Domain.Users;
    using Domain.Adverts;
    using Models;

    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<User, UserInfo>();
            Mapper.CreateMap<Service, ServiceSerializeInfo>()
                .ForMember(si => si.Lat, opt => opt.MapFrom(s => s.Location.X))
                .ForMember(si => si.Lng, opt => opt.MapFrom(s => s.Location.Y));
        }
    }
}