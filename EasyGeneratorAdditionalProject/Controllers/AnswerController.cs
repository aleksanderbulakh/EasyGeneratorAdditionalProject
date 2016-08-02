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
        public AnswerController(IUnitOfWork work, IAnswerRepository answerRepository,
            IMapper mapper, IDateConvertor convertor) 
            : base(work)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpPost]
        [Route("answer/edit/text", Name = "EditAnswerText")]
        public JsonResult EditSectionTitle(QuestionAnswer answer, User user, string text)
        {
            if (answer == null)
                return FailResult("question not find.");

            if (user == null)
                return FailResult("user not find.");

            answer.UpdateText(text, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(answer.Question.LastModifiedDate));
        }

        [HttpPost]
        [Route("answer/edit/state", Name = "EditAnswerState")]
        public JsonResult EditSectionState(QuestionAnswer answer, User user, bool state)
        {
            if (answer == null)
                return FailResult("question not find.");

            if (user == null)
                return FailResult("user not find.");

            answer.UpdateState(state, user.UserName);

            return SuccessResult(_convertor.ConvertDateToMilliseconds(answer.Question.LastModifiedDate));
        }

        [HttpPost]
        [Route("answer/delete", Name = "DeleteAnswer")]
        public JsonResult DeleteSection(QuestionAnswer answer)
        {
            if (answer != null)
                _answerRepository.Delete(answer);

            return SuccessResult(true);
        }

        [HttpPost]
        [Route("answer/create", Name = "CreateAnswer")]
        public JsonResult CreateSection(Question question, User user)
        {
            if (question == null)
                return FailResult("Question not find.");

            if (user == null)
                return FailResult("User not find.");

            var newAnswer = new QuestionAnswer("answer text", user.UserName, question, false);

            _answerRepository.Create(newAnswer);

            return SuccessResult(_mapper.Map<AnswerViewModel>(newAnswer));
        }

        [HttpGet]
        [Route("answer/list", Name = "AnswerList")]
        public JsonResult SectionsList(Question question)
        {
            if (question == null)
                return FailResult("Section is not found.");

            var sections = new List<AnswerViewModel>();

            foreach (var answer in question.AnswersCollection)
            {
                sections.Add(_mapper.Map<AnswerViewModel>(answer));
            }

            return SuccessResult(sections);
        }
    }
}