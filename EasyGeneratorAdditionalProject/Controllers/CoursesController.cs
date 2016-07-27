using AutoMapper;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.Convertors;
using EasyGeneratorAdditionalProject.Web.JsonResults;
using EasyGeneratorAdditionalProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class CoursesController : MainController
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IDateConvertor _convertor;
        public CoursesController(IUnitOfWork work, IUserRepository userRepository, ICourseRepository courseRepository, IMapper mapper, IDateConvertor convertor)
            :base(work)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("course/edit/title", Name = "EditCourseTitle")]
        public JsonResult EditCourseTitle(Course course, string userId, string title)
        {
            if (course == null)
                return new JsonFailedResult("Course not find.");
            
            var guidUserId = Guid.Empty;
            var tryGetUserId = Guid.TryParse(userId, out guidUserId);

            if (!tryGetUserId)
                return new JsonFailedResult("User is not found");

            var user = _userRepository.GetById(guidUserId);

            if (user == null)
                return new JsonFailedResult("User is not found");

            course.UpdateTitle(title, user.UserName);

            return new JsonSuccessResult(_convertor.ConvertDateToMilliseconds(course.LastModifiedDate));
        }

        [HttpPost]
        [Route("course/edit/description", Name = "EditCourseDescription")]
        public JsonResult EditCourseDescription(Course course, string userId, string description)
        {
            if (course == null)
                return new JsonFailedResult("Course not find.");

            var guidUserId = Guid.Empty;
            var tryGetUserId = Guid.TryParse(userId, out guidUserId);

            if (!tryGetUserId)
                return new JsonFailedResult("User is not found");

            var user = _userRepository.GetById(guidUserId);

            if (user == null)
                return new JsonFailedResult("User is not found");

            course.UpdateDescription(description, user.UserName);

            return new JsonSuccessResult(_convertor.ConvertDateToMilliseconds(course.LastModifiedDate));
        }

        [HttpPost]
        [Route("course/delete", Name = "DeleteCourse")]
        public JsonResult DeleteCourse(Course course)
        {

            if (course != null)
                _courseRepository.Delete(course);

            return new JsonSuccessResult(true);
        }

        [HttpPost]
        [Route("course/create", Name = "CreateCourse")]
        public JsonResult CreateCourse(User user)
        {
            if (user == null)
                return new JsonFailedResult("User is not found.");

            var newCourse = new Course("course title", "course description", user);

            _courseRepository.Create(newCourse);

            return new JsonSuccessResult(_mapper.Map<CourseViewModel>(newCourse));
        }

        [HttpGet]
        [Route("course/list", Name = "CoursesList")]
        public JsonResult CoursesList(User user)
        {
            if (user == null)
                return new JsonFailedResult("User is not found.");

            var courses = new List<CourseViewModel>();
            var courseCollection = user.CoursesCollection;

            foreach (var course in courseCollection)
            {
                courses.Add(_mapper.Map<CourseViewModel>(course));
            }

            return new JsonSuccessResult(courses);
        }
    }
}