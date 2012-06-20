

namespace AtYourService.Web.Util
{
    using AutoMapper;
    using Domain.Users;
    using Models;

    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<User, UserInfo>();
        }
    }
}