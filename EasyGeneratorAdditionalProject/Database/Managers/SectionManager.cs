using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Managers
{
    public class SectionManager : IDisposable
    {
        private readonly ISectionsDataProvider _provider;
        public SectionManager(ISectionsDataProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<Sections> GetAllSections()
        {
            return _provider.GetAllSections();
        }

        public void CreateSection(Sections sectionModel)
        {
            _provider.CreateSection(sectionModel);
        }

        public IEnumerable<Sections> GetSectionsByCourseId(Guid id)
        {
            return _provider.GetSectionsByCourseId(id);
        }

        public Sections GetSectionById(Guid id)
        {
            return _provider.GetSectionById(id);
        }

        public void EditSection(Sections sectionModel)
        {
            _provider.EditSection(sectionModel);
        }

        public void DeleteSection(Guid id)
        {
            _provider.DeleteSection(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}