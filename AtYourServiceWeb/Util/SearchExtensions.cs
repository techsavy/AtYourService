
namespace AtYourService.Web.Util
{
    using NHibernate;
    using NHibernate.Search;

    public static class SearchExtensions
    {
        public static IFullTextSession FullTextSession(this ISession session)
        {
            return Search.CreateFullTextSession(session);
        }
    }
}