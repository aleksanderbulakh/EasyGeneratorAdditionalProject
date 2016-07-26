using AutoMapper;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.JsonResults;
using EasyGeneratorAdditionalProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CoursesController(IUserRepository userRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        private User GetFirstUser()
        {
            return _userRepository.GetById(Guid.Parse("9f9338ba-55ab-4d12-a28a-fff7e8b3bda3"));
        }

        [HttpPost]
        [Route("course/edit/title", Name = "EditCourseTitle")]
        public JsonResult EditCourseTitle(Course course, string title)
        {
            if (course == null)
                return new JsonFailedResult("Course not find.");

            course.UpdateTitle(title);

            return new JsonSuccessResult(course.LastModifiedDate);
        }

        [HttpPost]
        [Route("course/edit/description", Name = "EditCourseDescription")]
        public JsonResult EditCourseDescription(Course course, string description)
        {
            if (course == null)
                return new JsonFailedResult("Course not find.");

            course.UpdateDescription(description);

            return new JsonSuccessResult(course.LastModifiedDate);
        }

        [HttpPost]
        [Route("course/delete", Name = "DeleteCourse")]
        public JsonResult DeleteCourse(Course course)
        {

            if (course != null)
                _courseRepository.Delete(course);

            return new JsonSuccessResult("Course deleted.");
        }

        [HttpPost]
        [Route("course/create", Name = "CreateCourse")]
        public JsonResult CreateCourse()
        {
            var user = GetFirstUser();

            var newCourse = new Course("course title", "course description", user);

            _courseRepository.Create(newCourse);

            return new JsonSuccessResult(_mapper.Map<CourseViewModel>(newCourse));
        }

        [HttpGet]
        [Route("course/list", Name = "CoursesList")]
        public JsonResult CoursesList()
        {
            var user = GetFirstUser();

            var courses = new List<CourseViewModel>();

            foreach (var course in _courseRepository.GetByUserId(user.Id))
            {
                courses.Add(_mapper.Map<CourseViewModel>(course));
            }

            return new JsonSuccessResult(courses);
        }
    }
}