﻿

namespace AtYourService.Web.Util
{
    using AutoMapper;
    using Domain.Adverts;
    using Domain.Users;
    using Models;

    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<User, UserInfo>()
                .ForMember(ui => ui.IsAuthenticated, opt => opt.UseValue(true))
                .ForMember(ui => ui.IsAdmin, opt => opt.Condition((User u) => u is Administrator));

            Mapper.CreateMap<Service, ServiceSerializeInfo>()
                .ForMember(si => si.Lat, opt => opt.MapFrom(s => s.Location.X))
                .ForMember(si => si.Lng, opt => opt.MapFrom(s => s.Location.Y));
        }
    }
}