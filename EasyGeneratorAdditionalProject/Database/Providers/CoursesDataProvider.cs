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
    public class CoursesDataProvider : ICoursesDataProvider
    {
        private DatabaseContext _context;
        public CoursesDataProvider(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Courses> GetAllCourses()
        {
            return _context.Courses;
        }

        public void CreateCourse(Courses courseModel)
        {
            _context.Courses.Add(courseModel);
            _context.SaveChanges();
        }

        public IEnumerable<Courses> GetCoursesByUserId(Guid id)
        {
            return _context.Courses.Where(d => d.UserId.Equals(id));
        }

        public Courses GetCourseById(Guid id)
        {
            return _context.Courses.Find(id);
        }

        public void EditCourse(Courses courseModel)
        {
            _context.Entry(courseModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCourse(Guid id)
        {
            _context.Courses.Remove(_context.Courses.Find(id));
            _context.SaveChanges();
        }
    }
}