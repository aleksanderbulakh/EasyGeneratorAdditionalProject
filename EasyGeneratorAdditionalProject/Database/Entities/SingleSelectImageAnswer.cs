using EasyGeneratorAdditionalProject.Database.Models.EntitisParentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
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