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
        public CoursesController(IUnitOfWork work, IUserRepository userRepository, ICourseRepository courseRepository,
            IMapper mapper, IDateConvertor convertor, ISectionRepository sectionRepository,
            IQuestionRepository questionRepository, ISimpleSelectAnswerRepository answerRepository)
            : base(work, userRepository, sectionRepository, questionRepository, answerRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("course/edit/title", Name = "EditCourseTitle")]
        public JsonResult EditCourseTitle(Course course, string title)
        {
            var user = GetFirstUser();

            if (course == null || user == null)
                throw new ArgumentException();

            course.UpdateTitle(title, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(course.LastModifiedDate));
        }

        [HttpPost]
        [Route("course/edit/description", Name = "EditCourseDescription")]
        public JsonResult EditCourseDescription(Course course, string description)
        {
            var user = GetFirstUser();

            if (course == null || user == null)
                throw new ArgumentException();

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
        public JsonResult CreateCourse(string courseTitle)
        {
            var user = GetFirstUser();

            if (user == null)
                throw new ArgumentException();

            var newCourse = new Course(courseTitle, "course description", user);

            _courseRepository.Add(newCourse);

            var newSection = CreateSectionMethod(newCourse);

            CreateSimpleSelectQuestionMethod(newSection, "single");

            return SuccessResult(_mapper.Map<CourseViewModel>(newCourse));
        }

        [HttpGet]
        [Route("course/list", Name = "CoursesList")]
        public JsonResult CoursesList()
        {
            var user = GetFirstUser();

            if (user == null)
                throw new ArgumentException();

            var courses = new List<CourseViewModel>();

            foreach (var course in user.CoursesCollection)
            {
                courses.Add(_mapper.Map<CourseViewModel>(course));
            }

            return SuccessResult(courses);
        }
    }
}