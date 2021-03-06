﻿using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Interfaces
{
    public interface ISimpleSelectAnswerRepository : IRepository<SimpleSelectAnswer>
    {
        List<SimpleSelectAnswer> GetByQuestionId(Guid id);
    }
}
