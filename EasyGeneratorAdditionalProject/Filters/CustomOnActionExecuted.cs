using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Filters
{
    public class CustomOnActionExecuted : FilterAttribute, IActionFilter
    {
        private readonly IUnitOfWork _work;
        public CustomOnActionExecuted()
        {
            _work = DependencyResolver.Current.GetService<IUnitOfWork>();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            _work.Save();
        }
    }
}