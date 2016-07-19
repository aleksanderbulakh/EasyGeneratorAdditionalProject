using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using EasyGeneratorAdditionalProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        public CoursesController(IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        [HttpPost]
        [Route("course/edit", Name = "EditCourse")]
        public JsonResult EditCourse(EditeCourseViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(false, JsonRequestBehavior.AllowGet);

            var course = _courseRepository.GetCourseById(Guid.Parse(model.Id));
            course.Title = model.Title;
            course.Description = model.Description;

            _courseRepository.EditCourse(course);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("course/delete", Name = "DeleteCourse")]
        public JsonResult DeleteCourse(string id)
        {
            var result = _courseRepository.DeleteCourse(Guid.Parse(id));
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [Route("course/create", Name = "CreateCourse")]
        public JsonResult CreateCourse()
        {
            var db = new DatabaseContext();

            var user = db.Users.ToList()[0];

            var courseId = _courseRepository.CreateCourse(new Course
            {
                Title = "course title",
                Description = "course description",
                UserId = user.Id,
                CreatedOn = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });
            var course = _courseRepository.GetCourseById(courseId);

            var courseModel = new CourseModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreatedBy = user.FirstName + user.Surname,
                CreatedOn = course.CreatedOn.ToShortDateString(),
                LastModifiedDate = course.LastModifiedDate.ToShortDateString()
            };

            return Json(courseModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("course/list", Name = "CoursesList")]
        public JsonResult CoursesList()
        {
            var db = new DatabaseContext();

            var user = db.Users.ToList()[0];

            var courses = _courseRepository.GetCoursesByUserId(user.Id);

            var courseModelList = new List<CourseModel>();

            foreach (var course in courses)
            {
                var courseModel = new CourseModel
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    CreatedBy = user.FirstName + user.Surname,
                    CreatedOn = course.CreatedOn.ToShortDateString(),
                    LastModifiedDate = course.LastModifiedDate.ToShortDateString(),
                    SectionsList = null
                };
                courseModelList.Add(courseModel);
            }

            return Json(courseModelList, JsonRequestBehavior.AllowGet);
        }
    }
}



