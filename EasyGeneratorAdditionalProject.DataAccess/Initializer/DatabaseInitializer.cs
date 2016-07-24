using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Initializer
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            var role = new Role { Id = Guid.NewGuid(), Name = "Teacher" };
            db.Roles.Add(role);
            db.Users.Add(new User(role, "Aleksander", "Bulakh", "llutor.2013@gmail.com", "zarzar123"));
            base.Seed(db);
        }
    }
}
