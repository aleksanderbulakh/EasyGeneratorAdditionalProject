using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.ModelBinders
{
    public class ModelCreator<T> : IModelCreator<T> where T : class
    {
        private string propertyName;
        public ModelCreator()
        {
            if (typeof(T) == typeof(User))
                propertyName = "userId";
            if (typeof(T) == typeof(Course))
                propertyName = "courseId";
            if (typeof(T) == typeof(Section))
                propertyName = "sectionId";
            if (typeof(T) == typeof(Question))
                propertyName = "questionId";
        }
        public T TryCreateModel(IValueProvider valueProvider)
        {
            var _repository = DependencyResolver.Current.GetService<IRepository<T>>();

            Guid id = Guid.Empty;
            var tryGetId = Guid.TryParse((String)valueProvider.GetValue(propertyName).ConvertTo(typeof(String)), out id);

            if (!tryGetId)
                throw new ArgumentException("Invalid parameter type.");
            
            return _repository.GetById(id);
        }
    }
}