using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface ISectionsDataProvider
    {
        IEnumerable<Sections> GetAllSections();

        void CreateSection(Sections sectionModel);

        IEnumerable<Sections> GetSectionsByCourseId(Guid id);

        Sections GetSectionById(Guid id);

        void EditSection(Sections sectionModel);

        void DeleteSection(Guid id);
    }
}
