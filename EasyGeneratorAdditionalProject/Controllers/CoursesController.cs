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
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IDateConvertor _convertor;
        public CoursesController(IUnitOfWork work, ICourseRepository courseRepository, IMapper mapper,
            IDateConvertor convertor)
            :base(work)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("course/edit/title", Name = "EditCourseTitle")]
        public JsonResult EditCourseTitle(Course course, User user, string title)
        {
            if (course == null)
                return FailResult("Course not find.");

            if (user == null)
                return FailResult("user not find.");

            course.UpdateTitle(title, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(course.LastModifiedDate));
        }

        [HttpPost]
        [Route("course/edit/description", Name = "EditCourseDescription")]
        public JsonResult EditCourseDescription(Course course, User user, string description)
        {
            if (course == null)
                return FailResult("Course not find.");

            if (user == null)
                return FailResult("user not find.");

            course.UpdateDescription(description, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(course.LastModifiedDate));
        }

        [HttpPost]
        [Route("course/delete", Name = "DeleteCourse")]
        public JsonResult DeleteCourse(Course course)
        {

            if (course != null)
                _courseRepository.Delete(course);

            return SuccessResult(true);
        }

        [HttpPost]
        [Route("course/create", Name = "CreateCourse")]
        public JsonResult CreateCourse(User user, string courseTitle)
        {
            if (user == null)
                return FailResult("User is not found.");

            var newCourse = new Course(courseTitle, "course description", user);

            _courseRepository.Create(newCourse);

            return SuccessResult(_mapper.Map<CourseViewModel>(newCourse));
        }

        [HttpGet]
        [Route("course/list", Name = "CoursesList")]
        public JsonResult CoursesList(User user)
        {
            if (user == null)
                return FailResult("User is not found.");

            var courses = new List<CourseViewModel>();
            var courseCollection = user.CoursesCollection;

            foreach (var course in courseCollection)
            {
                courses.Add(_mapper.Map<CourseViewModel>(course));
            }

            return SuccessResult(courses);
        }
    }
}