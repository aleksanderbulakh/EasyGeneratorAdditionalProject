using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IMaterialRepository
    {
        IEnumerable<Material> GetAllMaterials();
        void CreateMaterials(Material materialsModel);
        Material GetMaterialByContentId(Guid id);
        Material GetMaterialsById(Guid id);
        void EditMaterials(Material materialsModel);
        void DeleteMaterial(Guid id);
    }
}
