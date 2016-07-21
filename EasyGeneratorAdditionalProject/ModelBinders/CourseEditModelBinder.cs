using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.DataAccess.UnitOfWork;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.ModelBinders
{
    public class CourseEditModelBinder : IModelBinder
    {
        
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;

            var courseId = (Guid)valueProvider.GetValue("courseId").ConvertTo(typeof(Guid));

            if (courseId == null)
                return new ArgumentException("Don't have id");

            var _courseRepository = DependencyResolver.Current.GetService<IRepository<Course>>();
            return _courseRepository.GetById(courseId);
        }
    }
}