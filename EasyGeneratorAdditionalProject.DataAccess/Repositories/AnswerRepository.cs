using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Repositories
{
    public class AnswerRepository : Repository<SimpleSelectAnswers>, IAnswerRepository
    {
        public AnswerRepository(IDatabaseContext context)
            : base(context) { }

        public List<SimpleSelectAnswers> GetByQuestionId(Guid id)
        {
            return Entity().Where(t => t.Question.Id == id).ToList();
        }
    }
}
