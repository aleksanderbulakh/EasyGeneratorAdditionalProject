using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Identifier
    {
        public Guid Id { get; protected internal set; }

        public Identifier()
        {
            Id = Guid.NewGuid();
        }
    }
}
