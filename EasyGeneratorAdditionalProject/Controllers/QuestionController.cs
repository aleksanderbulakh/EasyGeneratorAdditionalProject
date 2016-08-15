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
        public QuestionController(IUnitOfWork work, IUserRepository userRepository,
            ISectionRepository sectionRepository, IQuestionRepository questionRepository,
            IMapper mapper, IDateConvertor convertor, ISimpleSelectAnswerRepository answerRepository)
            : base(work, userRepository, sectionRepository, questionRepository, answerRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("question/edit/title", Name = "EditQuestionTitle")]
        public JsonResult EditQuestionTitle(Question question, string title)
        {
            var user = GetFirstUser();

            if (question == null || user == null)
                throw new ArgumentException();

            question.UpdateTitle(title, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(question.LastModifiedDate));
        }

        [HttpPost]
        [Route("question/delete", Name = "DeleteQuestion")]
        public JsonResult DeleteQuestion(Question question)
        {
            if (question != null)
                _questionRepository.Delete(question);

            return SuccessResult(true);
        }

        [HttpPost]
        [Route("question/create/simple-question", Name = "CreateSingleQuestion")]
        public JsonResult CreateSimpleQuestion(Section section, string type)
        {
            var newQuestion = CreateSimpleSelectQuestionMethod(section, type);

            return SuccessResult(_mapper.Map<QuestionViewModel>(newQuestion));
        }

        [HttpGet]
        [Route("question/list", Name = "QuestionList")]
        public JsonResult QuestionList(Section section)
        {
            if (section == null)
                throw new ArgumentException();

            var sections = new List<QuestionViewModel>();

            foreach (var question in section.QuestionCollection)
            {
                sections.Add(_mapper.Map<QuestionViewModel>(question));
            }

            return SuccessResult(sections);
        }
    }
}