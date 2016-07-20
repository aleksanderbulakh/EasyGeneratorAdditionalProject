using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> UserCollection { get; set; }

        public Role()
        {
            Id = Guid.NewGuid();
            UserCollection = new List<User>();
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}