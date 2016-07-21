using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Providers
{
    public class CoursesRepository : IRepository<Course>
    {
        private DatabaseContext _context;
        public CoursesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course Create(Course modelObj)
        {
            _context.Courses.Add(modelObj);
            _context.SaveChanges();

            var course = GetById(modelObj.Id);

            return course;
        }

        public List<Course> GetByForeignId(Guid id)
        {
            var courses = _context.Courses.Where(d => d.UserId.Equals(id));

            return courses.ToList();
        }

        public Course GetById(Guid id)
        {
            return _context.Courses.Find(id);
        }

        public void Edit(Course modelObj)
        {
            //var course = new Course
            //{
            //    Id = modelObj.Id,
            //    Title = modelObj.Title,
            //    UserId = modelObj.UserId,
            //    CreatedBy = modelObj.CreatedBy,
            //    CreatedOn = modelObj.CreatedOn,
            //    Description = modelObj.Description,
            //    LastModifiedDate = modelObj.LastModifiedDate,
            //    SectionsList = modelObj.SectionsList,
            //    User = modelObj.User
            //};

           // _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            _context.Courses.Remove(_context.Courses.Find(id));
            _context.SaveChanges();

            if (GetById(id) == null)
                return true;
            else
                return false;
        }
    }
}
