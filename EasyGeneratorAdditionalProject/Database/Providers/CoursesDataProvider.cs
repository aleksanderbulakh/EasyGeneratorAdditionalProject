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
        public IEnumerable<Courses> GetAllCourses()
        {
            using (var db = new DatabaseContext())
            {
                return db.Courses;
            }
        }

        public void CreateCourse(Courses courseModel)
        {
            using (var db = new DatabaseContext())
            {
                db.Courses.Add(courseModel);
                db.SaveChanges();
            }
        }

        public IEnumerable<Courses> GetCoursesByUserId(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                return db.Courses.Where(d => d.UserId.Equals(id));
            }
        }

        public Courses GetCourseById(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                return db.Courses.Find(id);
            }
        }

        public void EditCourse(Courses courseModel)
        {
            using (var db = new DatabaseContext())
            {
                db.Entry(courseModel).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCourse(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                db.Courses.Remove(db.Courses.Find(id));
                db.SaveChanges();
            }
        }
    }
}