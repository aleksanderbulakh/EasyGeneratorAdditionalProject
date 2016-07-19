using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface ISectionRepository
    {
        IEnumerable<Section> GetAllSections();

        void CreateSection(Section sectionModel);

        IEnumerable<Section> GetSectionsByCourseId(Guid id);

        Section GetSectionById(Guid id);

        void EditSection(Section sectionModel);

        void DeleteSection(Guid id);
    }
}
