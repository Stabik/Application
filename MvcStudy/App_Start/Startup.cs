using Autofac;
using Autofac.Integration.Mvc;
using ClassLibrary.model;
using ClassLibrary.model.Repository;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Owin;
using MvcStudy.App_Start;
using MvcStudy.Controllers;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Startup))]

    namespace MvcStudy.App_Start
    {
        public partial class Startup
        {
        public static void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var connectionString = ConfigurationManager.ConnectionStrings["MSSQL"];
            if (connectionString == null)
            {
                throw new Exception("Не найдена строка подключения");
            }

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Register(x =>
            {
                var cfg = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString.ConnectionString)
                        .Dialect<MsSql2012Dialect>())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Person>())
                    .CurrentSessionContext("call");
                var conf = cfg.BuildConfiguration();
                var schemeExport = new SchemaUpdate(conf);
                schemeExport.Execute(true, true);
                return cfg.BuildSessionFactory();
            }).As<ISessionFactory>().SingleInstance();
            containerBuilder
                .Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerRequest();
            containerBuilder.RegisterControllers(Assembly.GetAssembly(typeof(HomeController)));
            containerBuilder.RegisterModule(new AutofacWebTypesModule());
            containerBuilder.RegisterType<PersonRepository>();
            var types = typeof(Person).Assembly.GetTypes();
            foreach (var type in types)
            {
                var repositoryAttribute = type.GetCustomAttribute<RepositoryAttribute>();
                if (repositoryAttribute == null)
                {
                    continue;
                }
                containerBuilder.RegisterType(type);
            }

            var container = containerBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);
        }
    }
    }
