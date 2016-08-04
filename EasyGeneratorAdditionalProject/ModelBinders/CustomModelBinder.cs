using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.ModelBinders
{
    public class CustomModelBinder<T> : IModelBinder where T : class
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var _repository = DependencyResolver.Current.GetService<IRepository<T>>();

            var valueProvider = bindingContext.ValueProvider;

            Guid id = Guid.Empty;
            var tryGetId = Guid.TryParse((String)valueProvider.GetValue(bindingContext.ModelName + "Id")
                .ConvertTo(typeof(String)), out id);

            if (!tryGetId)
                throw new ArgumentException("Invalid parameter type.");

            return _repository.GetById(id);
        }
    }
}