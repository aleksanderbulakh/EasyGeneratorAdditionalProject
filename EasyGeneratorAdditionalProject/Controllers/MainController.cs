using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.Web.JsonResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.Controllers
{
    public class MainController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly IUserRepository _userRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ISimpleSelectAnswerRepository _simpleSelectAnswerRepository;
        public MainController(IUnitOfWork work, IUserRepository userRepository, ISectionRepository sectionRepository,
            IQuestionRepository questionRepository, ISimpleSelectAnswerRepository simpleSelectAnswerRepository)
        {
            _work = work;
            _userRepository = userRepository;
            _sectionRepository = sectionRepository;
            _questionRepository = questionRepository;
            _simpleSelectAnswerRepository = simpleSelectAnswerRepository;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _work.Save();
            base.OnActionExecuted(filterContext);
        }

        protected JsonSuccessResult SuccessResult(object data)
        {
            return new JsonSuccessResult(data);
        }

        protected JsonFailedResult FailResult(string data)
        {
            return new JsonFailedResult(data);
        }

        protected User GetFirstUser()
        {
            return _userRepository.GetById(Guid.Parse("757de169-255e-4e77-b160-2b5ce2323856"));
        }

        protected Section CreateSectionMethod(Course course)
        {
            var user = GetFirstUser();

            if (course == null || user == null)
                throw new ArgumentException();

            var newSection = new Section("section title", user.UserName, course);

            _sectionRepository.Add(newSection);

            return newSection;
        }

        protected Question CreateSimpleSelectQuestionMethod(Section section, string type)
        {
            var user = GetFirstUser();

            if (section == null || user == null)
                throw new ArgumentException();

            var newQuestion = new Question("question title", user.UserName, section, type);

            _questionRepository.Add(newQuestion);

            CreateSimpleSelectAnswer(newQuestion, true);
            CreateSimpleSelectAnswer(newQuestion, false);

            return newQuestion;
        }

        protected SimpleSelectAnswer CreateSimpleSelectAnswer(Question question, bool isCorrect)
        {
            var user = GetFirstUser();

            if (question == null || user == null)
                throw new ArgumentException();

            var newAnswer = new SimpleSelectAnswer("answer text", user.UserName, question, isCorrect);

            _simpleSelectAnswerRepository.Add(newAnswer);

            return newAnswer;
        }
    }
}