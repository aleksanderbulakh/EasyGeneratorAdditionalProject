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
    public class RolesRepository : IRoleRepository
    {
        private DatabaseContext _context;
        public RolesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles;
        }

        public Role GetRoleById(Guid id)
        {
            return _context.Roles.Find(id);
        }

        public void CreateRole(Role modelObj)
        {
            _context.Roles.Add(modelObj);
            _context.SaveChanges();
        }

        public void EditRole(Role modelObj)
        {
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteRole(Guid id)
        {
            _context.Roles.Remove(_context.Roles.Find(id));
            _context.SaveChanges();
        }
    }
}