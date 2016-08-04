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
            if (bindingContext.ModelType == typeof(User))
            {
                return DependencyResolver.Current.GetService<IModelCreator<User>>().TryCreateModel(bindingContext);
            }

            if (bindingContext.ModelType == typeof(Course))
            {
                return DependencyResolver.Current.GetService<IModelCreator<Course>>().TryCreateModel(bindingContext);
            }

            if (bindingContext.ModelType == typeof(Section))
            {
                return DependencyResolver.Current.GetService<IModelCreator<Section>>().TryCreateModel(bindingContext);
            }

            if (bindingContext.ModelType == typeof(Question))
            {
                return DependencyResolver.Current.GetService<IModelCreator<Question>>().TryCreateModel(bindingContext);
            }

            if (bindingContext.ModelType == typeof(QuestionAnswer))
            {
                return DependencyResolver.Current.GetService<IModelCreator<QuestionAnswer>>().TryCreateModel(bindingContext);
            }

            return new ArgumentException("Invalid parameter.");
        }
    }
}