using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Providers
{
    public class MaterialsRepository : IMaterialRepository
    {
        private DatabaseContext _context;
        public MaterialsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Material> GetAllMaterials()
        {
            return _context.Materials;
        }

        public void CreateMaterials(Material modelObj)
        {
            _context.Materials.Add(modelObj);
            _context.SaveChanges();
        }

        public Material GetMaterialByContentId(Guid id)
        {
            return _context.Materials.First(d => d.ContentId.Equals(id));
        }

        public Material GetMaterialsById(Guid id)
        {
            return _context.Materials.Find(id);
        }

        public void EditMaterials(Material modelObj)
        {
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMaterial(Guid id)
        {
            _context.Materials.Remove(_context.Materials.Find(id));
            _context.SaveChanges();
        }
    }
}