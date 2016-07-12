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
    public class MultipleSelectQuestionDataProvider : IMultipleSelectQuestionDataProvider
    {
        private DatabaseContext _context;
        public MultipleSelectQuestionDataProvider(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<MultipleSelectQuestion> GetAllMultipleSelectQuestion()
        {
            return _context.MultipleSelectQuestion;
        }

        public void CreateMultipleSelectQuestion(MultipleSelectQuestion multipleSelectQuestionModel)
        {
            _context.MultipleSelectQuestion.Add(multipleSelectQuestionModel);
            _context.SaveChanges();
        }

        public IEnumerable<MultipleSelectQuestion> GetMultipleSelectQuestionByContentId(Guid id)
        {
            return _context.MultipleSelectQuestion.Where(d => d.ContentId.Equals(id));
        }

        public MultipleSelectQuestion GetMultipleSelectQuestionById(Guid id)
        {
            return _context.MultipleSelectQuestion.Find(id);
        }

        public void EditMultipleSelectQuestion(MultipleSelectQuestion multipleSelectQuestionModel)
        {
            _context.Entry(multipleSelectQuestionModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMultipleSelectQuestion(Guid id)
        {
            _context.MultipleSelectQuestion.Remove(_context.MultipleSelectQuestion.Find(id));
            _context.SaveChanges();
        }
    }
}