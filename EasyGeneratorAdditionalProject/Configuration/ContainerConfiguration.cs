using Autofac;
using Autofac.Integration.Mvc;
using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Entities;
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

            builder.RegisterType<RolesRepository>().As<IRoleRepository>();
            builder.RegisterType<UsersRepository>().As<IUserRepository>();
            builder.RegisterType<CoursesRepository>().As<IRepository<Course>>();
            builder.RegisterType<SectionsRepository>().As<ISectionRepository>();
            builder.RegisterType<ContentRepository>().As<IContentRepository>();
            builder.RegisterType<MaterialsRepository>().As<IMaterialRepository>();
            builder.RegisterType<SingleSelectAnswerRepository>().As<ISingleSelectAnswerRepository>();
            builder.RegisterType<MultipleSelectAnswerRepository>().As<IMultipleSelectAnswerRepository>();
            builder.RegisterType<SingleSelectImageAnswerRepository>().As<ISingleSelectImageAnswerRepository>();
            builder.RegisterType<DatabaseContext>().As<DatabaseContext>();
            
            //builder.Register(c => Mapper.Instance).As<IMapper>().InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}