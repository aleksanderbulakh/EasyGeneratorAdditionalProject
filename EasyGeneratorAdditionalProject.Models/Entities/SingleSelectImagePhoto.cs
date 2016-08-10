using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class SingleSelectImagePhoto : Identifier
    {
        public string Photo { get; protected internal set; }

        public SingleSelectImagePhoto(Guid answerId, string photo)
        {
            Id = answerId;
            Photo = photo;
        }
    }
}
