using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Role : Identifier
    {
        public string Name { get; protected internal set; }

        public virtual ICollection<User> UserCollection { get; protected internal set; }

        public Role()
            : base()
        {
            UserCollection = new List<User>();
        }

        public Role(string name)
            : this()
        {
            Name = name;
        }
    }
}
