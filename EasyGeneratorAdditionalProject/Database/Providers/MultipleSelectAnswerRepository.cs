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
    public class MultipleSelectAnswerRepository : IMultipleSelectAnswerRepository
    {
        private DatabaseContext _context;
        public MultipleSelectAnswerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<MultipleSelectAnswer> GetAllMultipleSelectQuestion()
        {
            return _context.MultipleSelectAnswers;
        }

        public void CreateMultipleSelectQuestion(MultipleSelectAnswer modelObj)
        {
            _context.MultipleSelectAnswers.Add(modelObj);
            _context.SaveChanges();
        }

        public IEnumerable<MultipleSelectAnswer> GetMultipleSelectQuestionByContentId(Guid id)
        {
            return _context.MultipleSelectAnswers.Where(d => d.ContentId.Equals(id));
        }

        public MultipleSelectAnswer GetMultipleSelectQuestionById(Guid id)
        {
            return _context.MultipleSelectAnswers.Find(id);
        }

        public void EditMultipleSelectQuestion(MultipleSelectAnswer modelObj)
        {
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMultipleSelectQuestion(Guid id)
        {
            _context.MultipleSelectAnswers.Remove(_context.MultipleSelectAnswers.Find(id));
            _context.SaveChanges();
        }
    }
}