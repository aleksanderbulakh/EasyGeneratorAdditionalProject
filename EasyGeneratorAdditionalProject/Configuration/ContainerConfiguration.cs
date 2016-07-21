using Autofac;
using Autofac.Integration.Mvc;
using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.DataAccess.Providers;
using EasyGeneratorAdditionalProject.Models.Entities;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Configuration
{
    public class ContainerConfiguration
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<RolesRepository>().As<IRepository<Role>>();
            builder.RegisterType<UsersRepository>().As<IRepository<User>>();
            builder.RegisterType<CoursesRepository>().As<IRepository<Course>>();
            builder.RegisterType<DatabaseContext>().As<DatabaseContext>();
            
            //builder.Register(c => Mapper.Instance).As<IMapper>().InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}