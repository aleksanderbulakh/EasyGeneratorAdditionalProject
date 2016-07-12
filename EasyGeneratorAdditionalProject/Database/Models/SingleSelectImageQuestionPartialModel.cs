using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Models
{
    public class SingleSelectImageQuestionPartialModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsAnswer { get; set; }
        public string Photo { get; set; }
    }
}