using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.ModelBinders
{
    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            if (!typeof(Identifier).IsAssignableFrom(modelType))
                return null;

            Type modelBinderType = typeof(CustomModelBinder<>)
                .MakeGenericType(modelType);

            var modelBinder = Activator.CreateInstance(modelBinderType);

            return (IModelBinder)modelBinder;
        }
    }
}