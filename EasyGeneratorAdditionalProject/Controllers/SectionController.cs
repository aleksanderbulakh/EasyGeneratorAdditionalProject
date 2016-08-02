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
        public SectionController(IUnitOfWork work, ICourseRepository courseRepository, 
            ISectionRepository sectionRepository, IMapper mapper, IDateConvertor convertor)
            : base(work)
        {
            _courseRepository = courseRepository;
            _sectionRepository = sectionRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("section/edit/title", Name = "EditSectionTitle")]
        public JsonResult EditSectionTitle(Section section, User user, string title)
        {
            if (section == null)
                return FailResult("section not find.");

            if (user == null)
                return FailResult("user not find.");

            section.UpdateTitle(title, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(section.LastModifiedDate));
        }

        [HttpPost]
        [Route("section/delete", Name = "DeleteSection")]
        public JsonResult DeleteSection(Section section)
        {
            if (section != null)
                _sectionRepository.Delete(section);

            return SuccessResult(true);
        }

        [HttpPost]
        [Route("section/create", Name = "CreateSection")]
        public JsonResult CreateSection(Course course, User user)
        {
            if (course == null)
                return FailResult("Section not find.");

            if (user == null)
                return FailResult("user not find.");

            var newSection = new Section("section title", user.UserName, course);

            _sectionRepository.Add(newSection);

            return SuccessResult(_mapper.Map<SectionViewModel>(newSection));
        }

        [HttpGet]
        [Route("section/list", Name = "SectionList")]
        public JsonResult SectionsList(Course course)
        {
            if (course == null)
                return FailResult("Course is not found.");

            var sections = new List<SectionViewModel>();

            foreach (var section in course.SectionsList)
            {
                sections.Add(_mapper.Map<SectionViewModel>(section));
            }

            return SuccessResult(sections);
        }
    }
}