﻿using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.DataAccess.Repositories;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.Convertors;
using EasyGeneratorAdditionalProject.Web.ModelBinders;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Configuration
{
    public class ContainerConfiguration
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            builder.RegisterType<DatabaseContext>()
                .As<IDatabaseContext>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<CourseRepository>().As<ICourseRepository>();
            builder.RegisterType<SectionRepository>().As<ISectionRepository>();
            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>();
            builder.RegisterType<SimpleSelectAnswerRepository>().As<ISimpleSelectAnswerRepository>();

            builder.RegisterType<DateConvertor>().As<IDateConvertor>().InstancePerLifetimeScope();

            builder.Register(c => Mapper.Instance).As<IMapper>().InstancePerLifetimeScope();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}