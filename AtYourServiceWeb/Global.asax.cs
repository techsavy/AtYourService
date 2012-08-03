

namespace AtYourService.Web
{
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Core;
    using Core.Geo;
    using Data;
    using Geo;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Tool.hbm2ddl;
    using Security;
    using Util;

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
                name: "UserLocatioin",
                url: "*/UpdateLocation/",
                defaults: new { controller = "Accounts", action = "UpdateLocation" }
            );

            routes.MapRoute(
                name: "CategoryBrowse",
                url: "Services/Category/{id}/{name}",
                defaults: new { controller = "Services", action = "Category", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ServiceDetails",
                url: "Services/Details/{id}/{title}",
                defaults: new { controller = "Services", action = "Details", title = UrlParameter.Optional }
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
            //var update = new SchemaUpdate(config);
            //update.Execute(false, true);

            var sessionFactory = config.BuildSessionFactory();
        }

        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetAssembly(typeof(MvcApplication)));
            builder.Register(x => new NHibernateConfigurator().GetSessionFactory()).SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).InstancePerHttpRequest();

            builder.RegisterType<AccountMembershipService>().As<IMembershipService>();
            builder.RegisterType<FormsAuthenticationService>().As<IFormsAuthenticationService>();
            builder.RegisterType<GeoCodingSerive>().As<IGeoCodingService>();

            builder.RegisterType<NHibernateContext>().AsSelf()
                .WithParameter((info, context) => info.Name == "userName", (info, context) => HttpContext.Current.User.Identity.Name);
            builder.RegisterType<WindowsFileSystem>().As<IFileSystem>()
                .WithParameter((info, context) => true, (info, context) => new HttpServerUtilityWrapper(HttpContext.Current.Server));

            builder.RegisterModule(new AutofacWebTypesModule());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AutoMapperConfiguration.Configure();
        }
    }
}