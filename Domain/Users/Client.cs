
namespace AtYourService.Domain.Users
{
    using Adverts;
    using Iesi.Collections.Generic;
    using Moderation;

    public class Client : User
    {
        public virtual ClientSettings ClientSettings { get; set; }

        public virtual ISet<Service> Services { get; set; }

        public virtual ISet<Review> Reviews { get; set; }
    }
}