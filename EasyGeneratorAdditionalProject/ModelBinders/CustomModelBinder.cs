using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.ModelBinders
{
    public class CustomModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;

            var parameterType = (String)valueProvider.GetValue("parameterType").ConvertTo(typeof(String));

            Guid id = Guid.Empty;
            var tryGetId = Guid.TryParse((String)valueProvider.GetValue(parameterType).ConvertTo(typeof(String)), out id);

            if (!tryGetId)
                return new ArgumentException("Invalid parameter type.");

            var _currentDependencyResolver = DependencyResolver.Current;

            switch (parameterType)
            {
                case "courseId":
                    {
                        var _courseRepository = _currentDependencyResolver.GetService<ICourseRepository>();
                        return _courseRepository.GetById(id);
                    }

                case "sectionId":
                    {
                        var _sectionRepository = _currentDependencyResolver.GetService<ISectionRepository>();
                        return _sectionRepository.GetById(id);
                    }

                case "userId":
                    {
                        var _userRepository = _currentDependencyResolver.GetService<IUserRepository>();
                        return _userRepository.GetById(id);
                    }

                default: return new ArgumentException("Invalid parameter type.");
            }
        }
    }
}