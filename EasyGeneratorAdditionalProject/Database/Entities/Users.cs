using EasyGeneratorAdditionalProject.Database.Hasher;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Users : IUser<Guid>
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Roles Role { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Courses> CoursesCollection { get; set; }        

        public Users()
        {
            CoursesCollection = new List<Courses>();
        }

        public Users(Guid roleId, string firstName, string surname, string email, string password)
        {
            Id = Guid.NewGuid();
            RoleId = roleId;
            FirstName = firstName;
            Surname = surname;
            UserName = firstName + " " + surname;
            Email = email;
            PasswordHash = new CustomPasswordHasher().HashPassword(password);
        }
    }
}