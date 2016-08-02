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
        public QuestionRepository(IDatabaseContext context)
            : base(context)
        { }

        public List<Question> GetBySectionId(Guid id)
        {
            return Entity().Where(t => t.Section.Id == id).ToList();
        }
    }
}
