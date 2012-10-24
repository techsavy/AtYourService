

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

            var markDown = new MarkdownSharp.Markdown();
            Mapper.CreateMap<Service, ServiceSerializeInfo>()
                .ForMember(si => si.Lat, opt => opt.MapFrom(s => s.Location.Y))
                .ForMember(si => si.Lng, opt => opt.MapFrom(s => s.Location.X))
                .ForMember(si => si.Body, opt => opt.MapFrom(s => markDown.Transform(s.Body)));

            Mapper.CreateMap<User, EditUserModel>()
                .ForMember(si => si.Latitude, opt => opt.MapFrom(s => s.Location.Y))
                .ForMember(si => si.Longitude, opt => opt.MapFrom(s => s.Location.X));
        }
    }
}