using Autofac;
using Autofac.Integration.Mvc;
using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using EasyGeneratorAdditionalProject.Database.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Configuration
{
    public class ContainerConfiguration
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<RolesDataProvider>().As<IRolesDataProvider>();
            builder.RegisterType<UsersDataProvider>().As<IUsersDataProvider>();
            builder.RegisterType<CoursesDataProvider>().As<ICoursesDataProvider>();
            builder.RegisterType<DatabaseContext>().As<DatabaseContext>();
            
            //builder.Register(c => Mapper.Instance).As<IMapper>().InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}