using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IMultipleSelectAnswerRepository
    {
        IEnumerable<MultipleSelectAnswer> GetAllMultipleSelectQuestion();
        void CreateMultipleSelectQuestion(MultipleSelectAnswer multipleSelectQuestionModel);
        IEnumerable<MultipleSelectAnswer> GetMultipleSelectQuestionByContentId(Guid id);
        MultipleSelectAnswer GetMultipleSelectQuestionById(Guid id);
        void EditMultipleSelectQuestion(MultipleSelectAnswer multipleSelectQuestionModel);
        void DeleteMultipleSelectQuestion(Guid id);
    }
}
