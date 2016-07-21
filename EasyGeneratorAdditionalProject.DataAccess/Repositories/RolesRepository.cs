using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace EasyGeneratorAdditionalProject.DataAccess.Repositories
{
    public class RolesRepository : IRepository<Role>
    {
        private IDatabaseContext _context;
        public RolesRepository(IDatabaseContext context)
        {
            _context = context;
        }

        private IDbSet<Role> Entity() 
        {
            return _context.GetSet<Role>();
        }

        public List<Role> GetAll()
        {
            var roles = Entity();

            var rolesList = new List<Role>();

            foreach (var role in roles)
            {
                rolesList.Add(role);
            }

            return rolesList;
        }

        public Role GetById(Guid id)
        {
            return Entity().Find(id);
        }

        public void Create(Role modelObj)
        {
            Entity().Add(modelObj);
        }

        public void Delete(Role modelObj)
        {
            Entity().Remove(modelObj);
        }

        public List<Role> GetByForeignId(Guid id)
        {
            return null;
        }
    }
}
