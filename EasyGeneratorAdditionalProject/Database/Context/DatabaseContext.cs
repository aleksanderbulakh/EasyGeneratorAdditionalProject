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
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<SingleSelectAnswer> SingleSelectAnswers { get; set; }
        public DbSet<MultipleSelectAnswer> MultipleSelectAnswers { get; set; }
        public DbSet<SingleSelectImageAnswer> SingleSelectImageAnswers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Role
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Role>().HasKey(t => t.Id);
            modelBuilder.Entity<Role>().HasMany(t => t.UserCollection);
            #endregion

            #region User
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(t => t.Id);
            modelBuilder.Entity<User>().Property(t => t.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.Surname).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.Email).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Ignore(t => t.UserName);
            modelBuilder.Entity<User>().HasMany(t => t.CoursesCollection);
            #endregion

            #region Course
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasKey(t => t.Id);
            modelBuilder.Entity<Course>().Property(t => t.Title).IsRequired();
            modelBuilder.Entity<Course>().Property(t => t.Title).HasMaxLength(255);
            modelBuilder.Entity<Course>().HasMany(t => t.SectionsList);
            #endregion

            #region Section
            modelBuilder.Entity<Section>().ToTable("Sections");
            modelBuilder.Entity<Section>().HasKey(t => t.Id);
            modelBuilder.Entity<Section>().Property(t => t.Title).IsRequired();
            modelBuilder.Entity<Section>().Property(t => t.Title).HasMaxLength(255);
            modelBuilder.Entity<Section>().HasMany(t => t.ContentCollection);
            #endregion

            #region Content
            modelBuilder.Entity<Content>().ToTable("Content");
            modelBuilder.Entity<Content>().HasKey(t => t.Id);
            modelBuilder.Entity<Content>().Property(t => t.Title).IsRequired();
            modelBuilder.Entity<Content>().Property(t => t.Title).HasMaxLength(255);
            modelBuilder.Entity<Content>().Property(t => t.Type).IsRequired();
            modelBuilder.Entity<Content>().HasMany(t => t.MaterialsCollection);
            modelBuilder.Entity<Content>().HasMany(t => t.SingleSelectAnswerCollection);
            modelBuilder.Entity<Content>().HasMany(t => t.MultipleSelectAnswerCollection);
            modelBuilder.Entity<Content>().HasMany(t => t.SingleSelectImageAnswerCollection);
            #endregion

            #region Material
            modelBuilder.Entity<Material>().ToTable("Materials");
            modelBuilder.Entity<Material>().HasKey(t => t.Id);
            modelBuilder.Entity<Material>().Property(t => t.Text).IsRequired();

            #endregion

            #region Single select answer
            modelBuilder.Entity<SingleSelectAnswer>().ToTable("SingleSelectAnswers");
            modelBuilder.Entity<SingleSelectAnswer>().HasKey(t => t.Id);
            modelBuilder.Entity<SingleSelectAnswer>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<SingleSelectAnswer>().Property(t => t.IsCorrect).IsRequired();
            #endregion

            #region Multiple select answer
            modelBuilder.Entity<MultipleSelectAnswer>().ToTable("MultipleSelectAnswers");
            modelBuilder.Entity<MultipleSelectAnswer>().HasKey(t => t.Id);
            modelBuilder.Entity<MultipleSelectAnswer>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<MultipleSelectAnswer>().Property(t => t.IsCorrect).IsRequired();
            #endregion

            #region Single Select Image Answer
            modelBuilder.Entity<SingleSelectImageAnswer>().ToTable("SingleSelectImageAnswers");
            modelBuilder.Entity<SingleSelectImageAnswer>().HasKey(t => t.Id);
            modelBuilder.Entity<SingleSelectImageAnswer>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<SingleSelectImageAnswer>().Property(t => t.IsCorrect).IsRequired();
            modelBuilder.Entity<SingleSelectImageAnswer>().Property(t => t.Photo).IsRequired();
            #endregion
        }
    }
}