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
    public class SectionsRepository : ISectionRepository
    {
        private DatabaseContext _context;
        public SectionsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Section> GetAllSections()
        {
            return _context.Sections;
        }

        public void CreateSection(Section modelObj)
        {
            _context.Sections.Add(modelObj);
            _context.SaveChanges();
        }

        public IEnumerable<Section> GetSectionsByCourseId(Guid id)
        {
            return _context.Sections.Where(d => d.CourseId.Equals(id));
        }

        public Section GetSectionById(Guid id)
        {
            return _context.Sections.Find(id);
        }

        public void EditSection(Section modelObj)
        {
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteSection(Guid id)
        {
            _context.Sections.Remove(_context.Sections.Find(id));
            _context.SaveChanges();
        }
    }
}