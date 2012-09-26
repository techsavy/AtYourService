// -----------------------------------------------------------------------
// <copyright file="NHibernateConfigurator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Data
{
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Engine;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Search.Event;
    using Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class NHibernateConfigurator
    {
        public NHibernate.Cfg.Configuration Configure()
        {
            var config = new NHibernate.Cfg.Configuration();
            config.SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof (NHibernate.Spatial.Dialect.MsSql2008GeographyDialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof (NHibernate.Driver.SqlClientDriver).AssemblyQualifiedName);

            var modelMapper = new ModelMapper(new ModelInspector());
            modelMapper.AddMappings(typeof(ModelInspector).Assembly.GetExportedTypes());

            var mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            config.AddMapping(mapping);

            config.SetListener(NHibernate.Event.ListenerType.PostUpdate, new FullTextIndexEventListener());
            config.SetListener(NHibernate.Event.ListenerType.PostInsert, new FullTextIndexEventListener());
            config.SetListener(NHibernate.Event.ListenerType.PostDelete, new FullTextIndexEventListener());

            return config;
        }

        public ISessionFactory GetSessionFactory()
        {
            var configuration = Configure();
            return configuration.BuildSessionFactory();
        }
    }
}
