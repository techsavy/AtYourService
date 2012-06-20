using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AtYourService.Data;
using AtYourService.Web.Security;
using AtYourService.Web.Util;
using Autofac;
using Autofac.Integration.Mvc;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace AtYourService.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.EnableBootstrapBundle();

            ConfigureDependencies();
            //NHibernateConfiguration();
        }

        private void NHibernateConfiguration()
        {
            var config = new Configuration();
            config.AddAssembly(typeof(MvcApplication).Assembly);
            config.AddAssembly(typeof(MsSqlSpatialDialect).Assembly);
            var modelMapper = new ModelMapper(new ModelInspector());
            modelMapper.AddMappings(typeof(ModelInspector).Assembly.GetExportedTypes());

            var mapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            config.AddMapping(mapping);

            var schema = new SchemaExport(config);
            schema.SetOutputFile("D:\\script.sql");
            schema.Create(true, true);

            var sessionFactory = config.BuildSessionFactory();
        }

        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetAssembly(typeof(MvcApplication)));
            builder.Register(x => new NHibernateConfigurator().GetSessionFactory())
                .SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .InstancePerHttpRequest();

            builder.RegisterType<AccountMembershipService>().As<IMembershipService>();
            builder.RegisterType<FormsAuthenticationService>().As<IFormsAuthenticationService>();
            builder.RegisterType<NHibernateContext>().AsSelf();

            builder.RegisterModule(new AutofacWebTypesModule());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AutoMapperConfiguration.Configure();
        }
    }
}