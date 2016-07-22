using AutoMapper;
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
        private readonly IUnitOfWork _work;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IMapper _mapper;
        public CoursesController(IUnitOfWork work, IRepository<User> userRepository, IRepository<Course> courseRepository, IMapper mapper)
        {
            _work = work;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        private User GetFirstUser()
        {
            return _userRepository.GetAll()[0];
        }

        [HttpPost]
        [Route("course/edit/title", Name = "EditCourseTitle")]
        public JsonResult EditCourseTitle(Course course, string title)
        {
            var requestResult = new List<object>();

            if (course == null)
            {
                requestResult.Add(false);
                requestResult.Add("Course not find.");
                return Json(requestResult, JsonRequestBehavior.AllowGet);
            }

            //DoAction(() =>
            //{
            //    course.UpdateTitle(title);

            //    requestResult.Add(true);
            //    requestResult.Add("Title changed.");
            //    return requestResult;

            //});

            try
            {
                course.UpdateTitle(title);

                requestResult.Add(true);
                requestResult.Add("Title changed.");
                return Json(requestResult, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException exception)
            {
                requestResult.Add(false);
                requestResult.Add(exception.Message);
                return Json(requestResult, JsonRequestBehavior.AllowGet);
            }
        }

        class JsonFailedResult : JsonResult {
            public JsonFailedResult(string errorMessage)
            {
                this.Data = new { Success = false, ErrorMessage = errorMessage };
            }
        }

        class JsonSuccessResult : JsonResult
        {
            public JsonSuccessResult(object data)
            {
                this.Data = new { Success = true, Data = data };
            }
        }

        ActionResult DoAction(Func<object> action) {
            try
            {
                var result = action();
                return new JsonSuccessResult(result);
            }
            catch (ArgumentException e) {
                return new JsonFailedResult(e.Message);
            }
        }

        [HttpPost]
        [Route("course/edit/description", Name = "EditCourseDescription")]
        public JsonResult EditCourseDescription(Course course, string description)
        {
            var requestResult = new List<object>();

            if (course == null)
            {
                requestResult.Add(false);
                requestResult.Add("Course not find.");
                return Json(requestResult, JsonRequestBehavior.AllowGet);
            }

            try
            {
                course.UpdateDescription(description);

                requestResult.Add(true);
                requestResult.Add("Description changed.");
                return Json(requestResult, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException exception)
            {
                requestResult.Add(false);
                requestResult.Add(exception.Message);
                return Json(requestResult, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("course/delete", Name = "DeleteCourse")]
        public JsonResult DeleteCourse(Course course)
        {
            var requestResult = new List<object>();

            if (course == null)
            {
                requestResult.Add(false);
                requestResult.Add("Course not find.");
                return Json(requestResult, JsonRequestBehavior.AllowGet);
            }

            _courseRepository.Delete(course);


            requestResult.Add(true);
            requestResult.Add("Course deleted.");
            return Json(requestResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("course/create", Name = "CreateCourse")]
        public JsonResult CreateCourse()
        {
            var user = GetFirstUser();

            try
            {
                var newCourse = new Course("course title", "course description", user.Id, user.FirstName + " " + user.Surname);

                _courseRepository.Create(newCourse);

                var courseViewModel = _mapper.Map<CourseViewModel>(newCourse);

                return Json(courseViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException exception)
            {
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
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
                var courseViewModel = _mapper.Map<CourseViewModel>(course);

                courseViewModelList.Add(courseViewModel);
            }

            return Json(courseViewModelList, JsonRequestBehavior.AllowGet);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _work.Save();
            base.OnActionExecuted(filterContext);
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    if (!filterContext.ExceptionHandled) {
        //        return;
        //    }

        //    if (filterContext.Exception is ArgumentException) {
        //        filterContext.Result = new JsonFailedResult(filterContext.Exception.Message);
        //    }
        //}
    }
}