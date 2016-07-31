using AutoMapper;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.Convertors;
using EasyGeneratorAdditionalProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class QuestionController : MainController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IDateConvertor _convertor;
        public QuestionController(IUnitOfWork work, IQuestionRepository questionRepository, 
            IMapper mapper, IDateConvertor convertor) 
            : base(work)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("question/edit/title", Name = "EditQuestionTitle")]
        public JsonResult EditSectionTitle(Question question, User user, string title)
        {
            if (question == null)
                return FailResult("question not find.");

            if (user == null)
                return FailResult("user not find.");

            question.UpdateTitle(title, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(question.LastModifiedDate));
        }

        [HttpPost]
        [Route("question/delete", Name = "DeleteQuestion")]
        public JsonResult DeleteSection(Question question)
        {
            if (question != null)
                _questionRepository.Delete(question);

            return SuccessResult(true);
        }

        [HttpPost]
        [Route("question/create", Name = "CreateQuestion")]
        public JsonResult CreateSection(Section section, User user)
        {
            if (section == null)
                return FailResult("Section not find.");

            if (user == null)
                return FailResult("User not find.");

            var newQuestion = new Question("section title", user.UserName, section);

            _questionRepository.Create(newQuestion);

            return SuccessResult(_mapper.Map<QuestionViewModel>(newQuestion));
        }

        [HttpGet]
        [Route("question/list", Name = "QuestionList")]
        public JsonResult SectionsList(Section section)
        {
            if (section == null)
                return FailResult("Section is not found.");

            var sections = new List<QuestionViewModel>();

            foreach (var question in section.QuestionCollection)
            {
                sections.Add(_mapper.Map<QuestionViewModel>(question));
            }

            return SuccessResult(sections);
        }
    }
}