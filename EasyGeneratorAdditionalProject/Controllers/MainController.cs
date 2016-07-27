using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Web.JsonResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class MainController : Controller
    {
        private readonly IUnitOfWork _work;
        public MainController(IUnitOfWork work)
        {
            _work = work;
        }
        // GET: Main
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _work.Save();
            base.OnActionExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = new JsonFailedResult(filterContext.Exception.Message);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}