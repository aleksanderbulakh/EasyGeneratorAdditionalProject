using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Users> UserCollection { get; set; }

        public Roles()
        {
            UserCollection = new List<Users>();
        }

        public Roles(string name)
        {
            Name = name;
        }
    }
}