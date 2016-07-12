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
    public class ContentDataProvider : IContentDataProvider
    {
        private DatabaseContext _context;
        public ContentDataProvider(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Content> GetAllContent()
        {
            return _context.Content;
        }

        public void CreateContent(Content contentModel)
        {
            _context.Content.Add(contentModel);
            _context.SaveChanges();
        }

        public IEnumerable<Content> GetContentBySectionId(Guid id)
        {
            return _context.Content.Where(d => d.SectionId.Equals(id));
        }

        public Content GetContentById(Guid id)
        {
            return _context.Content.Find(id);
        }

        public void EditContent(Content contentModel)
        {
            _context.Entry(contentModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteContent(Guid id)
        {
            _context.Content.Remove(_context.Content.Find(id));
            _context.SaveChanges();
        }
    }
}