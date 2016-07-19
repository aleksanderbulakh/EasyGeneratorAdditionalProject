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
    public class SingleSelectImageAnswerRepository : ISingleSelectImageAnswerRepository
    {
        private DatabaseContext _context;
        public SingleSelectImageAnswerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<SingleSelectImageAnswer> GetAllSingleSelectImageQuestions()
        {
            return _context.SingleSelectImageAnswers;
        }

        public void CreateSingleSelectImageQuestions(SingleSelectImageAnswer modelObj)
        {
            _context.SingleSelectImageAnswers.Add(modelObj);
            _context.SaveChanges();
        }

        public IEnumerable<SingleSelectImageAnswer> GetSingleSelectImageQuestionsByContentId(Guid id)
        {
            return _context.SingleSelectImageAnswers.Where(d => d.ContentId.Equals(id));
        }

        public SingleSelectImageAnswer GetSingleSelectImageQuestionsById(Guid id)
        {
            return _context.SingleSelectImageAnswers.Find(id);
        }

        public void EditSingleSelectImageQuestions(SingleSelectImageAnswer modelObj)
        {
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteSingleSelectImageQuestions(Guid id)
        {
            _context.SingleSelectImageAnswers.Remove(_context.SingleSelectImageAnswers.Find(id));
            _context.SaveChanges();
        }
    }
}