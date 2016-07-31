using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.ModelBinders
{
    interface IModelCreator <T> where T : class
    {
        T TryCreateModel(IValueProvider valueProvider);
    }
}
