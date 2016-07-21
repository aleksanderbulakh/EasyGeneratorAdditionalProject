using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Course> _courseRepository;
        public CoursesController(IRepository<User> userRepository, IRepository<Course> courseRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        private User GetFirstUser()
        {
            return _userRepository.GetAll()[0];
        }

        [HttpPost]
        [Route("course/edit/title", Name = "EditCourseTitle")]
        public JsonResult EditCourseTitle(Course course, string title)
        {
            if (course == null)
                return Json(false, JsonRequestBehavior.AllowGet);

            course.UpdateTitle(title);

            _courseRepository.Edit(course);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("course/edit/description", Name = "EditCourseDescription")]
        public JsonResult EditCourseDescription(Course course, string description)
        {
            if (course == null)
                return Json(false, JsonRequestBehavior.AllowGet);

            course.UpdateDescription(description);

            _courseRepository.Edit(course);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("course/delete", Name = "DeleteCourse")]
        public JsonResult DeleteCourse(string id)
        {
            var result = _courseRepository.Delete(Guid.Parse(id));
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [Route("course/create", Name = "CreateCourse")]
        public JsonResult CreateCourse()
        {
            var user = GetFirstUser();

            var newCourse = new Course
            {
                Title = "course title",
                Description = "course description",
                UserId = user.Id,
                CreatedOn = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                CreatedBy = user.FirstName + " " + user.Surname
            };

            var course = _courseRepository.Create(newCourse);

            var courseViewModel = new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreatedBy = course.CreatedBy,
                CreatedOn = course.CreatedOn.ToLongTimeString(),
                LastModifiedDate = course.LastModifiedDate.ToLongTimeString()
            };

            return Json(courseViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("course/list", Name = "CoursesList")]
        public JsonResult CoursesList()
        {
            var user = GetFirstUser();

            var courses = _courseRepository.GetByForeignId(user.Id);

            var courseViewModelList = new List<CourseViewModel>();

            foreach (var course in courses)
            {
                var courseViewModel = new CourseViewModel
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    CreatedBy = course.CreatedBy,
                    CreatedOn = course.CreatedOn.ToLongTimeString(),
                    LastModifiedDate = course.LastModifiedDate.ToLongTimeString()
                };
                courseViewModelList.Add(courseViewModel);
            }

            return Json(courseViewModelList, JsonRequestBehavior.AllowGet);
        }
    }
}