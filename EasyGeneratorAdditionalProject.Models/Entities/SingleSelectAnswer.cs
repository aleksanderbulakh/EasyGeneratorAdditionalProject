﻿using EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class SingleSelectAnswer : AnswersParentModel
    {
        public SingleSelectAnswer()
        {
            Id = Guid.NewGuid();
        }
    }
}