using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Repositories
{
    public class CoursesRepository : IRepository<Course>
    {
        private IDatabaseContext _context;
        public CoursesRepository(IDatabaseContext context)
        {
            _context = context;
        }

        private IDbSet<Course> Entity()
        {
            return _context.GetSet<Course>();
        }

        public List<Course> GetAll()
        {
            return Entity().ToList();
        }

        public void Create(Course modelObj)
        {
            Entity().Add(modelObj);
        }

        public List<Course> GetByForeignId(Guid id)
        {
            var courses = Entity().Where(d => d.UserId.Equals(id));

            return courses.ToList();
        }

        public Course GetById(Guid id)
        {
            return Entity().Find(id);
        }

        public void Delete(Guid id)
        {
            Entity().Remove(Entity().Find(id));
        }
    }
}
