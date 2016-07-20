using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using EasyGeneratorAdditionalProject.Database.Models;
using EasyGeneratorAdditionalProject.Database.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Database.ModelBinders
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