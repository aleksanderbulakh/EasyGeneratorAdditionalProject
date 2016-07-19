using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Initializer
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            var role = new Role { Id = Guid.NewGuid(), Name = "Teacher" };
            db.Roles.Add(role);
            db.Users.Add(new User(role.Id, "Aleksander", "Bulakh", "llutor.2013@gmail.com", "zarzar123"));
            base.Seed(db);
        }
    }
}