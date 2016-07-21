using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace EasyGeneratorAdditionalProject.DataAccess.Providers
{
    public class RolesRepository : IRepository<Role>
    {
        private DatabaseContext _context;
        public RolesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Role> GetAll()
        {
            var roles = _context.Roles;

            var rolesList = new List<Role>();

            foreach (var role in roles)
            {
                rolesList.Add(role);
            }

            return rolesList;
        }

        public Role GetById(Guid id)
        {
            return _context.Roles.Find(id);
        }

        public Role Create(Role modelObj)
        {
            _context.Roles.Add(modelObj);
            _context.SaveChanges();

            return GetById(modelObj.Id);
        }

        public void Edit(Role modelObj)
        {
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            _context.Roles.Remove(_context.Roles.Find(id));
            _context.SaveChanges();

            if (GetById(id) == null)
                return true;
            else
                return false;
        }

        public List<Role> GetByForeignId(Guid id)
        {
            return null;
        }
    }
}
