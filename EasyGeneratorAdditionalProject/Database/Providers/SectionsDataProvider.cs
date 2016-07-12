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
    public class SectionsDataProvider : ISectionsDataProvider
    {
        private DatabaseContext _context;
        public SectionsDataProvider(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Sections> GetAllSections()
        {
            return _context.Sections;
        }

        public void CreateSection(Sections sectionModel)
        {
            _context.Sections.Add(sectionModel);
            _context.SaveChanges();
        }

        public IEnumerable<Sections> GetSectionsByCourseId(Guid id)
        {
            return _context.Sections.Where(d => d.CourseId.Equals(id));
        }

        public Sections GetSectionById(Guid id)
        {
            return _context.Sections.Find(id);
        }

        public void EditSection(Sections sectionModel)
        {
            _context.Entry(sectionModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteSection(Guid id)
        {
            _context.Sections.Remove(_context.Sections.Find(id));
            _context.SaveChanges();
        }
    }
}