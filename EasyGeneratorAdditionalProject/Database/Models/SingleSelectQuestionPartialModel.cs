using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Models
{
    public class SingleSelectQuestionPartialModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsAnswer { get; set; }
    }
}