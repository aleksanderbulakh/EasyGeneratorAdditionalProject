using EasyGeneratorAdditionalProject.Database.Hasher;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class User : IUser<Guid>
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Course> CoursesCollection { get; set; }        

        public User()
        {
            CoursesCollection = new List<Course>();
        }

        public User(Guid roleId, string firstName, string surname, string email, string password)
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