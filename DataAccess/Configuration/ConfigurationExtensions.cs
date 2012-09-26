// -----------------------------------------------------------------------
// <copyright file="ConfigurationExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data.Configuration
{
    using System;
    using NHibernate.Cfg;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static Configuration AddNamedSqlQuery(this Configuration configuration, string queryIdentifier, Action<INamedSqlQueryDefinitionBuilder> namedQueryDefinition)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (queryIdentifier == null)
            {
                throw new ArgumentNullException("queryIdentifier");
            }

            if (namedQueryDefinition == null)
            {
                throw new ArgumentNullException("namedQueryDefinition");
            }

            var builder = new NamedSqlQueryDefinitionBuilder();
            namedQueryDefinition(builder);
            configuration.NamedSQLQueries.Add(queryIdentifier, builder.Build());

            return configuration;
        }
    }
}
