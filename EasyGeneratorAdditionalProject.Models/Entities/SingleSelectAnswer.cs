using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class SingleSelectAnswer : Answers
    {
        public SingleSelectAnswer()
        {
            Id = Guid.NewGuid();
        }
    }
}
