using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface ISingleSelectQuestionsDataProvider
    {
        IEnumerable<SingleSelectQuestions> GetAllSingleSelectQuestion();

        void CreateSingleSelectQuestion(SingleSelectQuestions singleSelectQuestionModel);

        IEnumerable<SingleSelectQuestions> GetSingleSelectQuestionByContentId(Guid id);

        SingleSelectQuestions GetSingleSelectQuestionById(Guid id);

        void EditSingleSelectQuestion(SingleSelectQuestions singleSelectQuestionsModel);

        void DeleteSingleSelectQuestion(Guid id);
    }
}
