using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Managers
{
    public class CoursesManager : IDisposable
    {
        private readonly ICoursesDataProvider _repository;
        public CoursesManager(ICoursesDataProvider repository)
        {
            _repository = repository;
        }

        public IEnumerable<Courses> GetAllCourses()
        {
            return _repository.GetAllCourses();
        }

        public void CreateCourse(Courses courseModel)
        {
            if (courseModel.Id == null)
                courseModel.Id = Guid.NewGuid();

            _repository.CreateCourse(courseModel);
        }

        public IEnumerable<Courses> GetCoursesByUserId(Guid id)
        {
            return _repository.GetCoursesByUserId(id);
        }

        public Courses GetCourseById(Guid id)
        {
            return _repository.GetCourseById(id);
        }

        public void EditCourse(Courses courseModel)
        {
            _repository.EditCourse(courseModel);
        }

        public void DeleteCourse(Guid id)
        {
            _repository.DeleteCourse(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}