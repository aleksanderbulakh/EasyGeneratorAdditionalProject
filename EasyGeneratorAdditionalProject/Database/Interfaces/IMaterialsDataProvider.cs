using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IMaterialsDataProvider
    {
        IEnumerable<Materials> GetAllMaterials();

        void CreateMaterials(Materials materialsModel);

        Materials GetMaterialByContentId(Guid id);

        Materials GetMaterialsById(Guid id);

        void EditMaterials(Materials materialsModel);

        void DeleteMaterial(Guid id);
    }
}
