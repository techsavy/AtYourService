

namespace AtYourService.Web.Util
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using NHibernate;
    using NHibernate.Criterion.Lambda;

    public static class QueryOverExtensions
    {
        public static IQueryOverOrderBuilder<TRoot, TSubType> OrderBy<TRoot, TSubType>(this IQueryOver<TRoot, TSubType> root, string propertyName)
        {
            var type = typeof (TSubType);
            var param = Expression.Parameter(type, "o");
            Expression body = Expression.PropertyOrField(param, propertyName);

            var memberInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (memberInfo.PropertyType.IsValueType)
            {
                body = Expression.Convert(body, typeof(object));
            }

            return new IQueryOverOrderBuilder<TRoot, TSubType>(root, Expression.Lambda<Func<TSubType, object>>(body, param));
        }
    }
}