using EasyGeneratorAdditionalProject.Configuration;
using EasyGeneratorAdditionalProject.Database.Initializer;
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
        }
    }
}
