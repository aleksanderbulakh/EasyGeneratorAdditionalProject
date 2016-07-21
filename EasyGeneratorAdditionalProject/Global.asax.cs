using EasyGeneratorAdditionalProject.DataAccess.Initializer;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.Configuration;
using EasyGeneratorAdditionalProject.Web.ModelBinders;
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
            MappingConfiguration.Configure();        

            AreaRegistration.RegisterAllAreas();

            ModelBinders.Binders.Add(typeof(Course), new CourseEditModelBinder());
        }
    }
}
