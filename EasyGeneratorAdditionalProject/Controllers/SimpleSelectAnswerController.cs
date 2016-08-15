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
    public class SimpleSelectAnswerController : MainController
    {
        private readonly ISimpleSelectAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly IDateConvertor _convertor;
        public SimpleSelectAnswerController(IUnitOfWork work, IUserRepository userRepository, ISimpleSelectAnswerRepository answerRepository,
            IMapper mapper, IDateConvertor convertor) 
            : base(work, userRepository)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("answer/edit/text", Name = "EditAnswerText")]
        public JsonResult EditAnswerText(SimpleSelectAnswer answer, string text)
        {
            var user = GetFirstUser();

            if (answer == null || user == null)
                throw new ArgumentException();

            answer.UpdateText(text, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(answer.Question.LastModifiedDate));
        }

        [HttpPost]
        [Route("answer/single/edit/state", Name = "EditSingleAnswerState")]
        public JsonResult EditSingleAnswerState(Question question, string answerId)
        {
            var user = GetFirstUser();

            if (question == null || user == null)
                throw new ArgumentException();

            question.SetCorrectAnswer(answerId, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(question.LastModifiedDate));
        }

        [HttpPost]
        [Route("answer/multiple/edit/state", Name = "EditMultipleAnswerState")]
        public JsonResult EditMultipleAnswerState(SimpleSelectAnswer answer, bool state)
        {
            var user = GetFirstUser();

            if (answer == null || user == null)
                throw new ArgumentException();

            answer.UpdateState(state);
            answer.MarkAsModified(user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(answer.Question.LastModifiedDate));
        }

        [HttpPost]
        [Route("answer/delete", Name = "DeleteAnswer")]
        public JsonResult DeleteAnswer(SimpleSelectAnswer answer)
        {
            if (answer != null)
                _answerRepository.Delete(answer);

            return SuccessResult(true);
        }

        [HttpPost]
        [Route("answer/create", Name = "CreateAnswer")]
        public JsonResult CreateAnswer(Question question)
        {
            var user = GetFirstUser();

            if (question == null || user == null)
                throw new ArgumentException();

            var newAnswer = new SimpleSelectAnswer("answer text", user.UserName, question, false);

            _answerRepository.Add(newAnswer);

            return SuccessResult(_mapper.Map<AnswerViewModel>(newAnswer));
        }

        [HttpGet]
        [Route("answer/list", Name = "AnswerList")]
        public JsonResult AnswersList(Question question)
        {
            if (question == null)
                throw new ArgumentException();

            var sections = new List<AnswerViewModel>();

            foreach (var answer in question.AnswersCollection)
            {
                sections.Add(_mapper.Map<AnswerViewModel>(answer));
            }

            return SuccessResult(sections);
        }
    }
}