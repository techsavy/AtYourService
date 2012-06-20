
namespace AtYourService.Domain.Users
{
    using Adverts;
    using Iesi.Collections.Generic;

    public class Client : User
    {
        public virtual ClientSettings ClientSettings { get; set; }

        public virtual ISet<Advert> Adverts { get; set; }
    }
}