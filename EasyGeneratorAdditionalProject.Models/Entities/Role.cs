using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Role : Identity
    {
        public string Name { get; set; }

        public virtual ICollection<User> UserCollection { get; set; }

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
