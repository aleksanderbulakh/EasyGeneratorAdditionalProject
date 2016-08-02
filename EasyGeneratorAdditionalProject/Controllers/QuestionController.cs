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
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly IDateConvertor _convertor;
        public QuestionController(IUnitOfWork work, IQuestionRepository questionRepository, 
            IMapper mapper, IDateConvertor convertor, IAnswerRepository answerRepository) 
            : base(work)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _convertor = convertor;
            _answerRepository = answerRepository;
        }

        [HttpPost]
        [Route("question/edit/title", Name = "EditQuestionTitle")]
        public JsonResult EditQuestionTitle(Question question, User user, string title)
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
        public JsonResult DeleteQuestion(Question question)
        {
            if (question != null)
                _questionRepository.Delete(question);

            return SuccessResult(true);
        }

        [HttpPost]
        [Route("question/create", Name = "CreateQuestion")]
        public JsonResult CreateQuestion(Section section, User user, string type)
        {
            if (section == null)
                return FailResult("Section not find.");

            if (user == null)
                return FailResult("User not find.");

            var newQuestion = new Question("question title", user.UserName, section, type);

            _questionRepository.Add(newQuestion);
            
            var answer = new QuestionAnswer("Question answer", newQuestion.CreatedBy, newQuestion, true);

            _answerRepository.Add(answer);

            answer = new QuestionAnswer("Question answer", newQuestion.CreatedBy, newQuestion, false);

            _answerRepository.Add(answer);

            return SuccessResult(_mapper.Map<QuestionViewModel>(newQuestion));
        }

        [HttpGet]
        [Route("question/list", Name = "QuestionList")]
        public JsonResult QuestionList(Section section)
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