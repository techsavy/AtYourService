
namespace AtYourService.Web.Models
{
    using NetTopologySuite.Geometries;

    public class UserInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Point Location { get; set; }

        public bool IsAuthenticated { get; set; }

        public bool IsAdmin { get; set; }
    }
}