using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    interface ICoursesDataProvider
    {
        IEnumerable<Courses> GetAllCourses();

        void CreateCourse(Courses courseModel);

        IEnumerable<Courses> GetCoursesByUserId(Guid id);

        Courses GetCourseById(Guid id);

        void EditCourse(Courses courseModel);

        void DeleteCourse(Guid id);
    }
}
