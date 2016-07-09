using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class SingleSelectImageQuestions
    {
        public Guid Id { get; set; }
        public Guid ContentId { get; set; }
        public Content Content { get; set; }
        public string Text { get; set; }
        public string IsAnswer { get; set; }
        public string Photo { get; set; }
    }
}