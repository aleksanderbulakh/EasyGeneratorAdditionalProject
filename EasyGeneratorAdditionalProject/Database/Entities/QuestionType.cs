using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class QuestionType
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }

        public QuestionType()
        {
            Questions = new List<Questions>();
        }
    }
}