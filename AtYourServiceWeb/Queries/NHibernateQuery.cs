
namespace AtYourService.Web.Queries
{
    using NHibernate;

    public interface IQuery<out T>
    {
        T Execute(ISession session);
    }
}
