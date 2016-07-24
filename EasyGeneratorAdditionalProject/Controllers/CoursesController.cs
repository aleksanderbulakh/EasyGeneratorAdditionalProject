using AutoMapper;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Models.Models;
using EasyGeneratorAdditionalProject.Web.JsonResults;
using EasyGeneratorAdditionalProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CoursesController(IUnitOfWork work, IUserRepository userRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _work = work;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        private User GetFirstUser()
        {
            return _userRepository.GetById(Guid.Parse("d4b0ce30-2555-4c74-9c83-34be6821112a"));
        }

        [HttpPost]
        [Route("course/edit/title", Name = "EditCourseTitle")]
        public JsonResult EditCourseTitle(Course course, string title)
        {
            course.UpdateTitle(title);

            return new JsonSuccessResult("Title changed.");
        }

        [HttpPost]
        [Route("course/edit/description", Name = "EditCourseDescription")]
        public JsonResult EditCourseDescription(Course course, string description)
        {

            if (course == null)
                return new JsonFailedResult("Course not find.");

            course.UpdateDescription(description);

            return new JsonSuccessResult("Description changed.");
        }

        [HttpPost]
        [Route("course/delete", Name = "DeleteCourse")]
        public JsonResult DeleteCourse(Course course)
        {

            if (course == null)
                return new JsonFailedResult("Course not find.");

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

            var r = DateTime.MinValue;
            var t = DateTime.Now;
            var tr = (t - r).Ticks/TimeSpan.TicksPerMillisecond;

            return new JsonSuccessResult(courses);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _work.Save();
            base.OnActionExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = new JsonFailedResult(filterContext.Exception.Message);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}