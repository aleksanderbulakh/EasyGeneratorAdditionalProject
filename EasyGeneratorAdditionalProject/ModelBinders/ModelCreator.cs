using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.ModelBinders
{
    public class ModelCreator<T> : IModelCreator<T> where T : class
    {
        public T TryCreateModel(ModelBindingContext bindingContext)
        {
            var _repository = DependencyResolver.Current.GetService<IRepository<T>>();

            Guid id = Guid.Empty;
            var tryGetId = Guid.TryParse((String)bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "id")
                .ConvertTo(typeof(String)), out id);

            if (!tryGetId)
                throw new ArgumentException("Invalid parameter type.");

            return _repository.GetById(id);
        }
    }
}