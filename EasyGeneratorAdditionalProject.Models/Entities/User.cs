using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class User : Identity, IUser<Guid>
    {
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<Course> CoursesCollection { get; set; }

        public string UserName
        {
            get
            {
                return String.Format("{0} {1}", FirstName, Surname);
            }
            set
            {

            }
        }

        public User()
            : base()
        {
            CoursesCollection = new List<Course>();
        }

        public User(Role role, string firstName, string surname, string email, string password)
            : this()
        {
            Role = role;
            FirstName = firstName;
            Surname = surname;
            UserName = firstName + " " + surname;
            Email = email;
            PasswordHash = password;
        }

    }
}
