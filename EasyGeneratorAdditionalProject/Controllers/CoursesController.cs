using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using EasyGeneratorAdditionalProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Course> _courseRepository;
        public CoursesController(IUserRepository userRepository, IRepository<Course> courseRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
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
            var user = _userRepository.GetFirstUser();

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

            var courseViewModel = new CourseModel
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
            var user = _userRepository.GetFirstUser();

            var courses = _courseRepository.GetByUserId(user.Id);

            var courseViewModelList = new List<CourseModel>();

            foreach (var course in courses)
            {
                var courseViewModel = new CourseModel
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