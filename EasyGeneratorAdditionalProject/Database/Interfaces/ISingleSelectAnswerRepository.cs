using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface ISingleSelectAnswerRepository
    {
        IEnumerable<SingleSelectAnswer> GetAllSingleSelectQuestion();

        void CreateSingleSelectQuestion(SingleSelectAnswer singleSelectQuestionModel);

        IEnumerable<SingleSelectAnswer> GetSingleSelectQuestionByContentId(Guid id);

        SingleSelectAnswer GetSingleSelectQuestionById(Guid id);

        void EditSingleSelectQuestion(SingleSelectAnswer singleSelectQuestionsModel);

        void DeleteSingleSelectQuestion(Guid id);
    }
}
