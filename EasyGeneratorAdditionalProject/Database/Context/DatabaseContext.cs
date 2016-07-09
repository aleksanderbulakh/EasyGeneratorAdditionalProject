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
        public DbSet<Content> Content { get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<SingleSelectQuestions> SingleSelectQuestion { get; set; }
        public DbSet<MultipleSelectQuestion> MultipleSelectQuestion { get; set; }
        public DbSet<SingleSelectImageQuestions> SingleSelectImageQuestions { get; set; }

    }
}