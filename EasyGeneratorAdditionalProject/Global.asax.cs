using EasyGeneratorAdditionalProject.DataAccess.Initializer;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.Configuration;
using EasyGeneratorAdditionalProject.Web.ModelBinders;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EasyGeneratorAdditionalProject
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DatabaseInitializer());

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfiguretion.Configure();
            ContainerConfiguration.Configure();
            MappingConfiguration.Configure();
            FilterConfiguration.RegisterGlobalFilters(GlobalFilters.Filters);

            AreaRegistration.RegisterAllAreas();

            ModelBinders.Binders.Add(typeof(Course), new ModelBinder());
            ModelBinders.Binders.Add(typeof(Section), new ModelBinder());
        }
    }
}
