using AutoMapper;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.JsonResults;
using EasyGeneratorAdditionalProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class SectionController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;
        public SectionController(ICourseRepository courseRepository, ISectionRepository sectionRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("section/edit/title", Name = "EditSectionTitle")]
        public JsonResult EditSectionTitle(Section section, string title)
        {
            if (section == null)
                return new JsonFailedResult("section not find.");

            section.UpdateTitle(title);

            return new JsonSuccessResult(section.LastModifiedDate);
        }

        [HttpPost]
        [Route("section/delete", Name = "DeleteSection")]
        public JsonResult DeleteSection(Section section)
        {
            if (section != null)
                _sectionRepository.Delete(section);

            return new JsonSuccessResult("section deleted.");
        }

        [HttpPost]
        [Route("section/create", Name = "CreateSection")]
        public JsonResult CreateSection(Course course)
        {
            if (course == null)
                return new JsonFailedResult("Section not find.");

            var newSection = new Section("section title", course);

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

            foreach (var section in _sectionRepository.GetByCourseId(course.Id))
            {
                sections.Add(_mapper.Map<SectionViewModel>(section));
            }

            return new JsonSuccessResult(sections);
        }
    }
}