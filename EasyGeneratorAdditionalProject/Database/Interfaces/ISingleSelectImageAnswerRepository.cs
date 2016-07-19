using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface ISingleSelectImageAnswerRepository
    {
        IEnumerable<SingleSelectImageAnswer> GetAllSingleSelectImageQuestions();

        void CreateSingleSelectImageQuestions(SingleSelectImageAnswer singleSelectImageQuestionsModel);

        IEnumerable<SingleSelectImageAnswer> GetSingleSelectImageQuestionsByContentId(Guid id);

        SingleSelectImageAnswer GetSingleSelectImageQuestionsById(Guid id);

        void EditSingleSelectImageQuestions(SingleSelectImageAnswer singleSelectImageQuestionsModel);

        void DeleteSingleSelectImageQuestions(Guid id);
    }
}
