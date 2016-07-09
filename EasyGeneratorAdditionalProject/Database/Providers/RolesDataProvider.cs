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
    public class RolesDataProvider : IRolesDataProvider
    {
        private DatabaseContext _context;
        public RolesDataProvider(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Roles> GetAllRoles()
        {
            return _context.Roles;
        }

        public Roles GetRoleById(Guid id)
        {
            return _context.Roles.Find(id);
        }

        public void CreateRole(Roles roleModel)
        {
            _context.Roles.Add(roleModel);
            _context.SaveChanges();
        }

        public void EditRole(Roles roleModel)
        {
            _context.Entry(roleModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteRole(Guid id)
        {
            _context.Roles.Remove(_context.Roles.Find(id));
            _context.SaveChanges();
        }
    }
}