using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyGeneratorAdditionalProject.Models.Entities;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;

namespace EasyGeneratorAdditionalProject.DataAccess.Context
{
    public class DatabaseContext : DbContext, IUnitOfWork, IDatabaseContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<SingleSelectImagePhoto> Photos { get; set; }

        public IDbSet<T> GetSet<T>() where T : class
        {
            return Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Role
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Role>().HasKey(t => t.Id);
            modelBuilder.Entity<Role>().HasMany(t => t.UserCollection).WithRequired(t => t.Role);
            #endregion

            #region User
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(t => t.Id);
            modelBuilder.Entity<User>().Property(t => t.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.Surname).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.Email).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Ignore(t => t.UserName);
            modelBuilder.Entity<User>().HasMany(t => t.CoursesCollection).WithRequired(t => t.User);
            #endregion

            #region Course
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasKey(t => t.Id);
            modelBuilder.Entity<Course>().Property(t => t.Title).IsRequired();
            modelBuilder.Entity<Course>().Property(t => t.Title).HasMaxLength(255);
            modelBuilder.Entity<Course>().HasMany(t => t.SectionsList).WithRequired(t => t.Course);
            #endregion

            #region Section
            modelBuilder.Entity<Section>().ToTable("Sections");
            modelBuilder.Entity<Section>().HasKey(t => t.Id);
            modelBuilder.Entity<Section>().Property(t => t.Title).IsRequired();
            modelBuilder.Entity<Section>().Property(t => t.Title).HasMaxLength(255);
            modelBuilder.Entity<Section>().HasMany(t => t.QuestionCollection).WithRequired(t => t.Section);
            #endregion

            #region Question
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Question>().HasKey(t => t.Id);
            modelBuilder.Entity<Question>().Property(t => t.Title).IsRequired();
            modelBuilder.Entity<Question>().Property(t => t.Title).HasMaxLength(255);
            modelBuilder.Entity<Question>().Property(t => t.Type).IsRequired();
            modelBuilder.Entity<Question>().HasMany(t => t.AnswersCollection).WithRequired(t => t.Question);
            #endregion

            #region Answers
            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<Answer>().HasKey(t => t.Id);
            modelBuilder.Entity<Answer>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<Answer>().Property(t => t.IsCorrect).IsRequired();
            modelBuilder.Entity<Answer>()
                .Map<SimpleSelectAnswer>(t => t.Requires("AnswerType").HasValue("simple"))
                .Map<SingleSelectImageAnswer>(t => t.Requires("AnswerType").HasValue("image"));
            #endregion

            #region Photos
            modelBuilder.Entity<SingleSelectImagePhoto>().ToTable("Photos");
            modelBuilder.Entity<SingleSelectImagePhoto>().HasKey(t => t.Id);
            modelBuilder.Entity<SingleSelectImagePhoto>().Property(t => t.Photo).IsRequired();
            #endregion
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}
