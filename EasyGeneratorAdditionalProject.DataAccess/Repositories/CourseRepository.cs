using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(IDatabaseContext context)
            : base(context) { }

        public List<Course> GetByUserId(Guid id)
        {
            return Entity().Where(t=>t.User.Id == id).ToList();
        }
    }
}
