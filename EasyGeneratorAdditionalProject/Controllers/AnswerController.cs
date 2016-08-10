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
    public class AnswerController : MainController
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly IDateConvertor _convertor;
        public AnswerController(IUnitOfWork work, IUserRepository userRepository, IAnswerRepository answerRepository,
            IMapper mapper, IDateConvertor convertor) 
            : base(work, userRepository)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("answer/edit/text", Name = "EditAnswerText")]
        public JsonResult EditAnswerText(SimpleSelectAnswers answer, string text)
        {
            var user = GetFirstUser();

            if (answer == null)
                return FailResult("question not find.");

            if (user == null)
                return FailResult("user not find.");

            answer.UpdateText(text, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(answer.Question.LastModifiedDate));
        }

        [HttpPost]
        [Route("answer/single/edit/state", Name = "EditSingleAnswerState")]
        public JsonResult EditSingleAnswerState(Question question, string answerId)
        {
            var user = GetFirstUser();

            if (question == null)
                return FailResult("question not find.");

            if (user == null)
                return FailResult("user not find.");

            question.ModifySingleSelectAnswersState(answerId, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(question.LastModifiedDate));
        }

        [HttpPost]
        [Route("answer/multiple/edit/state", Name = "EditMultipleAnswerState")]
        public JsonResult EditMultipleAnswerState(SimpleSelectAnswers answer, bool state)
        {
            var user = GetFirstUser();

            if (answer == null)
                return FailResult("question not find.");

            if (user == null)
                return FailResult("user not find.");

            answer.UpdateState(state);
            answer.MarkAsModified(user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(answer.Question.LastModifiedDate));
        }

        [HttpPost]
        [Route("answer/delete", Name = "DeleteAnswer")]
        public JsonResult DeleteAnswer(SimpleSelectAnswers answer)
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

            if (question == null)
                return FailResult("Question not find.");

            if (user == null)
                return FailResult("User not find.");

            var newAnswer = new SimpleSelectAnswers("answer text", user.UserName, question, false);

            _answerRepository.Add(newAnswer);

            return SuccessResult(_mapper.Map<AnswerViewModel>(newAnswer));
        }

        [HttpGet]
        [Route("answer/list", Name = "AnswerList")]
        public JsonResult AnswersList(Question question)
        {
            if (question == null)
                throw new ArgumentException("Question is not found.");

            var sections = new List<AnswerViewModel>();

            foreach (var answer in question.AnswersCollection)
            {
                sections.Add(_mapper.Map<AnswerViewModel>(answer));
            }

            return SuccessResult(sections);
        }
    }
}