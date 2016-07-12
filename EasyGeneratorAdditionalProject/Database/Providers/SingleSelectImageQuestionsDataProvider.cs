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
    public class SingleSelectImageQuestionsDataProvider : ISingleSelectImageQuestionsDataProvider
    {
        private DatabaseContext _context;
        public SingleSelectImageQuestionsDataProvider(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<SingleSelectImageQuestions> GetAllSingleSelectImageQuestions()
        {
            return _context.SingleSelectImageQuestions;
        }

        public void CreateSingleSelectImageQuestions(SingleSelectImageQuestions singleSelectImageQuestionsModel)
        {
            _context.SingleSelectImageQuestions.Add(singleSelectImageQuestionsModel);
            _context.SaveChanges();
        }

        public IEnumerable<SingleSelectImageQuestions> GetSingleSelectImageQuestionsByContentId(Guid id)
        {
            return _context.SingleSelectImageQuestions.Where(d => d.ContentId.Equals(id));
        }

        public SingleSelectImageQuestions GetSingleSelectImageQuestionsById(Guid id)
        {
            return _context.SingleSelectImageQuestions.Find(id);
        }

        public void EditSingleSelectImageQuestions(SingleSelectImageQuestions singleSelectImageQuestionsModel)
        {
            _context.Entry(singleSelectImageQuestionsModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteSingleSelectImageQuestions(Guid id)
        {
            _context.SingleSelectImageQuestions.Remove(_context.SingleSelectImageQuestions.Find(id));
            _context.SaveChanges();
        }
    }
}