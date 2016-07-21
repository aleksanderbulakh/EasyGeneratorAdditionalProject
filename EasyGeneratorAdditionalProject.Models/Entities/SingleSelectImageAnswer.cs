using EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class SingleSelectImageAnswer : AnswersParentModel
    {
        public string Photo { get; set; }

        public SingleSelectImageAnswer()
        {
            Id = Guid.NewGuid();
        }
    }
}
