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
    public class CoursesRepository : ICourseRepository
    {
        private DatabaseContext _context;
        public CoursesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses;
        }

        public Guid CreateCourse(Course modelObj)
        {
            modelObj.Id = Guid.NewGuid();

            _context.Courses.Add(modelObj);
            _context.SaveChanges();

            return modelObj.Id;
        }

        public IEnumerable<Course> GetCoursesByUserId(Guid id)
        {
            return _context.Courses.Where(d => d.UserId.Equals(id));
        }

        public Course GetCourseById(Guid id)
        {
            return _context.Courses.Find(id);
        }

        public void EditCourse(Course modelObj)
        {
            modelObj.LastModifiedDate = DateTime.Now;
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool DeleteCourse(Guid id)
        {
            _context.Courses.Remove(_context.Courses.Find(id));
            _context.SaveChanges();

            if (GetCourseById(id) == null)
                return true;
            else
                return false;
        }
    }
}