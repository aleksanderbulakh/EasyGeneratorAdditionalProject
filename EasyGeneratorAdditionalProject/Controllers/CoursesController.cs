using EasyGeneratorAdditionalProject.Database.Entities;
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
            var userId = Guid.Parse("625d5b37-a48b-42f8-be15-63187d81c9c0");
            
            using (var courseManager = new CoursesManager(_courseProvider))
            {
                var data = courseManager.GetCoursesByUserId(userId).ToList();
                
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}