using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels
{
    public class Identity
    {
        public Guid Id { get; set; }

        public Identity()
        {
            Id = Guid.NewGuid();
        }
    }
}
