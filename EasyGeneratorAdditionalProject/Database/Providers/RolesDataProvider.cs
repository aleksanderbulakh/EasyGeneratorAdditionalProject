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
        public IEnumerable<Roles> GetAllRoles()
        {
            using (var db = new DatabaseContext())
            {
                return db.Roles;
            }
        }

        public Roles GetRoleById(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                return db.Roles.Find(id);
            }
        }

        public void CreateRole(Roles roleModel)
        {
            using (var db = new DatabaseContext())
            {
                db.Roles.Add(roleModel);
                db.SaveChanges();
            }
        }

        public void EditRole(Roles roleModel)
        {
            using (var db = new DatabaseContext())
            {
                db.Entry(roleModel).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteRole(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                db.Roles.Remove(db.Roles.Find(id));
                db.SaveChanges();
            }
        }
    }
}