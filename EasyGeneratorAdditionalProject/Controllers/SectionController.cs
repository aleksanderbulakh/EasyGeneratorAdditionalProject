using AutoMapper;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.Convertors;
using EasyGeneratorAdditionalProject.Web.JsonResults;
using EasyGeneratorAdditionalProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class SectionController : MainController
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;
        private readonly IDateConvertor _convertor;
        public SectionController(IUnitOfWork work, ICourseRepository courseRepository, ISectionRepository sectionRepository, IUserRepository userRepository, IMapper mapper, IDateConvertor convertor)
            :base(work, userRepository)
        {
            _courseRepository = courseRepository;
            _sectionRepository = sectionRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("section/edit/title", Name = "EditSectionTitle")]
        public JsonResult EditSectionTitle(Section section, string userId, string title)
        {
            if (section == null)
                return new JsonFailedResult("section not find.");

            var user = ThrowIfUserDataInvalid(userId);

            section.UpdateTitle(title, user.UserName);

            return new JsonSuccessResult(_convertor.ConvertDateToMilliseconds(section.LastModifiedDate));
        }

        [HttpPost]
        [Route("section/delete", Name = "DeleteSection")]
        public JsonResult DeleteSection(Section section)
        {
            if (section != null)
                _sectionRepository.Delete(section);

            return new JsonSuccessResult(true);
        }

        [HttpPost]
        [Route("section/create", Name = "CreateSection")]
        public JsonResult CreateSection(Course course, string userId)
        {
            if (course == null)
                return new JsonFailedResult("Section not find.");

            var user = ThrowIfUserDataInvalid(userId);

            var newSection = new Section("section title", user.UserName, course);

            _sectionRepository.Create(newSection);

            return new JsonSuccessResult(_mapper.Map<SectionViewModel>(newSection));
        }

        [HttpGet]
        [Route("section/list", Name = "SectionList")]
        public JsonResult SectionsList(Course course)
        {
            if (course == null)
                return new JsonFailedResult("Course is not found.");

            var sections = new List<SectionViewModel>();

            foreach (var section in course.SectionsList)
            {
                sections.Add(_mapper.Map<SectionViewModel>(section));
            }

            return new JsonSuccessResult(sections);
        }
    }
}