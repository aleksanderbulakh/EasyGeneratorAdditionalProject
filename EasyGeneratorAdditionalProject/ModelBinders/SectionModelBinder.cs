using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.ModelBinders
{
    public class SectionModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;

            var sectionId = (Guid)valueProvider.GetValue("sectionId").ConvertTo(typeof(Guid));

            if (sectionId == null)
                return new ArgumentException("Don't have id");

            var _sectionRepository = DependencyResolver.Current.GetService<ISectionRepository>();
            return _sectionRepository.GetById(sectionId);
        }
    }
}