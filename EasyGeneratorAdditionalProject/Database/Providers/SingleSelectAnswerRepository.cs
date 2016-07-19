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
    public class SingleSelectAnswerRepository : ISingleSelectAnswerRepository
    {
        private DatabaseContext _context;
        public SingleSelectAnswerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<SingleSelectAnswer> GetAllSingleSelectQuestion()
        {
            return _context.SingleSelectAnswers;
        }

        public void CreateSingleSelectQuestion(SingleSelectAnswer modelObj)
        {
            _context.SingleSelectAnswers.Add(modelObj);
            _context.SaveChanges();
        }

        public IEnumerable<SingleSelectAnswer> GetSingleSelectQuestionByContentId(Guid id)
        {
            return _context.SingleSelectAnswers.Where(d => d.ContentId.Equals(id));
        }

        public SingleSelectAnswer GetSingleSelectQuestionById(Guid id)
        {
            return _context.SingleSelectAnswers.Find(id);
        }

        public void EditSingleSelectQuestion(SingleSelectAnswer modelObj)
        {
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteSingleSelectQuestion(Guid id)
        {
            _context.SingleSelectAnswers.Remove(_context.SingleSelectAnswers.Find(id));
            _context.SaveChanges();
        }
    }
}