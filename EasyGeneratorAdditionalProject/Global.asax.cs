using EasyGeneratorAdditionalProject.Configuration;
using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Initializer;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using EasyGeneratorAdditionalProject.Database.ModelBinders;
using EasyGeneratorAdditionalProject.Database.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EasyGeneratorAdditionalProject
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new DatabaseInitializer());

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfiguretion.Configure();
            ContainerConfiguration.Configure();            

            AreaRegistration.RegisterAllAreas();

            ModelBinders.Binders.Add(typeof(Course), new CourseEditModelBinder());
        }
    }
}
