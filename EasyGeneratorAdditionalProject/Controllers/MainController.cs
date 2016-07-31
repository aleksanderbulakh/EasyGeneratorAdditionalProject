using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
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
        
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _work.Save();
            base.OnActionExecuted(filterContext);
        }

        protected JsonSuccessResult SuccessResult(object data)
        {
            return new JsonSuccessResult(data);
        }

        protected JsonFailedResult FailResult(string data)
        {
            return new JsonFailedResult(data);
        }
    }
}