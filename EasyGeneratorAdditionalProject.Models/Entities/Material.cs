using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Material
    {
        public Guid Id { get; set; }
        public Content Content { get; set; }
        public string Text { get; set; }

        public Material()
        {
            Id = Guid.NewGuid();
        }
    }
}
