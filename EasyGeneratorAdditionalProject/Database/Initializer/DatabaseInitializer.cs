using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Initializer
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            var role = new Roles { Id = Guid.NewGuid(), Name = "Teacher" };
            db.Roles.Add(role);
            db.Users.Add(new Users(role.Id, "Aleksander", "Bulakh", "llutor.2013@gmail.com", "zarzar123"));
            base.Seed(db);
        }
    }
}