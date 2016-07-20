using EasyGeneratorAdditionalProject.Database.Models.EntitisParentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class MultipleSelectAnswer : AnswersParentModel
    {
        public MultipleSelectAnswer()
        {
            Id = Guid.NewGuid();
        }
    }
}