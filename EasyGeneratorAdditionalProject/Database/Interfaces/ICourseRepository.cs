using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses();
        Guid CreateCourse(Course courseModel);
        IEnumerable<Course> GetCoursesByUserId(Guid id);
        Course GetCourseById(Guid id);
        void EditCourse(Course courseModel);
        bool DeleteCourse(Guid id);
    }
}
