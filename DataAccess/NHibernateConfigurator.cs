// -----------------------------------------------------------------------
// <copyright file="NHibernateConfigurator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data
{
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Mapping.ByCode;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NHibernateConfigurator
    {
        public Configuration Configure()
        {
            var config = new Configuration();
            config.SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof (NHibernate.Spatial.Dialect.MsSql2008GeographyDialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof (NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);

            var modelMapper = new ModelMapper(new ModelInspector());
            modelMapper.AddMappings(typeof(ModelInspector).Assembly.GetExportedTypes());

            var mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            config.AddMapping(mapping);

            return config;
        }

        public ISessionFactory GetSessionFactory()
        {
            var configuration = Configure();
            return configuration.BuildSessionFactory();
        }
    }
}
