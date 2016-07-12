using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface ISingleSelectImageQuestionsDataProvider
    {
        IEnumerable<SingleSelectImageQuestions> GetAllSingleSelectImageQuestions();

        void CreateSingleSelectImageQuestions(SingleSelectImageQuestions singleSelectImageQuestionsModel);

        IEnumerable<SingleSelectImageQuestions> GetSingleSelectImageQuestionsByContentId(Guid id);

        SingleSelectImageQuestions GetSingleSelectImageQuestionsById(Guid id);

        void EditSingleSelectImageQuestions(SingleSelectImageQuestions singleSelectImageQuestionsModel);

        void DeleteSingleSelectImageQuestions(Guid id);
    }
}
