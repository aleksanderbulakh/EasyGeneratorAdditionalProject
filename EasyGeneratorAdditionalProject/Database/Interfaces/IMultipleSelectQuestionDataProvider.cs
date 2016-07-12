using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IMultipleSelectQuestionDataProvider
    {
        IEnumerable<MultipleSelectQuestion> GetAllMultipleSelectQuestion();

        void CreateMultipleSelectQuestion(MultipleSelectQuestion multipleSelectQuestionModel);

        IEnumerable<MultipleSelectQuestion> GetMultipleSelectQuestionByContentId(Guid id);

        MultipleSelectQuestion GetMultipleSelectQuestionById(Guid id);

        void EditMultipleSelectQuestion(MultipleSelectQuestion multipleSelectQuestionModel);

        void DeleteMultipleSelectQuestion(Guid id);
    }
}
