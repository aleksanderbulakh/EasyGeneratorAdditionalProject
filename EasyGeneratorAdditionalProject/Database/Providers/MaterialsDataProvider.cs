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
    public class MaterialsDataProvider : IMaterialsDataProvider
    {
        private DatabaseContext _context;
        public MaterialsDataProvider(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Materials> GetAllMaterials()
        {
            return _context.Materials;
        }

        public void CreateMaterials(Materials materialsModel)
        {
            _context.Materials.Add(materialsModel);
            _context.SaveChanges();
        }

        public Materials GetMaterialByContentId(Guid id)
        {
            return _context.Materials.First(d => d.ContentId.Equals(id));
        }

        public Materials GetMaterialsById(Guid id)
        {
            return _context.Materials.Find(id);
        }

        public void EditMaterials(Materials materialsModel)
        {
            _context.Entry(materialsModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMaterial(Guid id)
        {
            _context.Materials.Remove(_context.Materials.Find(id));
            _context.SaveChanges();
        }
    }
}