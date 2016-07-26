using EasyGeneratorAdditionalProject.Web.JsonResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Filters
{
    public class CustomOnException : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
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