using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly IAnswerRepository _answerRepository;
        public QuestionRepository(IDatabaseContext context, IAnswerRepository answerRepository)
            : base(context)
        {
            _answerRepository = answerRepository;
        }

        public List<Question> GetBySectionId(Guid id)
        {
            return Entity().Where(t => t.Section.Id == id).ToList();
        }

        public void CreateSomeStandartAnswers(Question question)
        {
            var answer = new QuestionAnswer("Question answer", question.CreatedBy, question, true);

            _answerRepository.Create(answer);

            answer = new QuestionAnswer("Question answer", question.CreatedBy, question, false);

            _answerRepository.Create(answer);
        }
    }
}
