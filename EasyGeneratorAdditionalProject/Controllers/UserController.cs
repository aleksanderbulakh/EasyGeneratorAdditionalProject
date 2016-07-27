using AutoMapper;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.JsonResults;
using EasyGeneratorAdditionalProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class UserController : MainController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork work, IUserRepository userRepository, IMapper mapper)
            :base(work)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        private User GetFirstUser()
        {
            return _userRepository.GetById(Guid.Parse("9f9338ba-55ab-4d12-a28a-fff7e8b3bda3"));
        }

        [HttpGet]
        [Route("user/get", Name = "LoadUserData")]
        public JsonResult GetUserData()
        {
            var user = GetFirstUser();

            return new JsonSuccessResult(_mapper.Map<UserViewModel>(user));
        }
    }
}