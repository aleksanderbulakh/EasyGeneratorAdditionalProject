using EasyGeneratorAdditionalProject.Database.Interfaces;
using EasyGeneratorAdditionalProject.Database.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesDataProvider _courseProvider;
        private readonly IUsersDataProvider _userProvider;
        public CoursesController(ICoursesDataProvider courseProvider, IUsersDataProvider userProvider)
        {
            _courseProvider = courseProvider;
            _userProvider = userProvider;
        }

        [Route("courses", Name = "coursesList")]
        public JsonResult CoursesList()
        {
            using (var usersManager = new UsersManager(_userProvider))
            {
                return Json((usersManager.GetAllUsers().ToList())[0].CoursesCollection, JsonRequestBehavior.AllowGet);
            }
        }
    }
}