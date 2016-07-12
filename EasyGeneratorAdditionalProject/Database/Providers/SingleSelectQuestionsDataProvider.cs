using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Providers
{
    public class SingleSelectQuestionsDataProvider : ISingleSelectQuestionsDataProvider
    {
        private DatabaseContext _context;
        public SingleSelectQuestionsDataProvider(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<SingleSelectQuestions> GetAllSingleSelectQuestion()
        {
            return _context.SingleSelectQuestion;
        }

        public void CreateSingleSelectQuestion(SingleSelectQuestions singleSelectQuestionModel)
        {
            _context.SingleSelectQuestion.Add(singleSelectQuestionModel);
            _context.SaveChanges();
        }

        public IEnumerable<SingleSelectQuestions> GetSingleSelectQuestionByContentId(Guid id)
        {
            return _context.SingleSelectQuestion.Where(d => d.ContentId.Equals(id));
        }

        public SingleSelectQuestions GetSingleSelectQuestionById(Guid id)
        {
            return _context.SingleSelectQuestion.Find(id);
        }

        public void EditSingleSelectQuestion(SingleSelectQuestions singleSelectQuestionsModel)
        {
            _context.Entry(singleSelectQuestionsModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteSingleSelectQuestion(Guid id)
        {
            _context.SingleSelectQuestion.Remove(_context.SingleSelectQuestion.Find(id));
            _context.SaveChanges();
        }
    }
}