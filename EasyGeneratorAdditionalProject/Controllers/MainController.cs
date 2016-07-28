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

        protected User ThrowIfUserDataInvalid(string userId)
        {
            var guidUserId = Guid.Empty;
            var tryGetUserId = Guid.TryParse(userId, out guidUserId);

            if (!tryGetUserId)
                throw new ArgumentException("User is not found");

            var user = _userRepository.GetById(guidUserId);

            if (user == null)
                throw new ArgumentException("User is not found");

            return user;
        }
    }
}