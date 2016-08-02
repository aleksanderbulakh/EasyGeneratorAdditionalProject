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
            

            if (bindingContext.ModelType == typeof(User))
            {
                return DependencyResolver.Current.GetService<IModelCreator<User>>().TryCreateModel(valueProvider);
            }

            if (bindingContext.ModelType == typeof(Course))
            {
                return DependencyResolver.Current.GetService<IModelCreator<Course>>().TryCreateModel(valueProvider);
            }

            if (bindingContext.ModelType == typeof(Section))
            {
                return DependencyResolver.Current.GetService<IModelCreator<Section>>().TryCreateModel(valueProvider);
            }

            if (bindingContext.ModelType == typeof(Question))
            {
                return DependencyResolver.Current.GetService<IModelCreator<Question>>().TryCreateModel(valueProvider);
            }

            if (bindingContext.ModelType == typeof(QuestionAnswer))
            {
                return DependencyResolver.Current.GetService<IModelCreator<QuestionAnswer>>().TryCreateModel(valueProvider);
            }

            return new ArgumentException("Invalid parameter.");
        }
    }
}