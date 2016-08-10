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
        private readonly IUserRepository _userRepository;
        public MainController(IUnitOfWork work, IUserRepository userRepository)
        {
            _work = work;
            _userRepository = userRepository;
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

        protected User GetFirstUser()
        {
            return _userRepository.GetById(Guid.Parse("9f9338ba-55ab-4d12-a28a-fff7e8b3bda3"));
        }
    }
}