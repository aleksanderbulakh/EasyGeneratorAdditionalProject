using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Sections> Sections { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Questions> Questions { get; set; }
    }
}