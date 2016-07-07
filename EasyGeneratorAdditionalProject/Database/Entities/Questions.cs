using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Questions
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public Sections Section { get; set; }
        public Guid QuestionTypeId { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Title { get; set; }
        public int Answer { get; set; }
        public string AnswerVariants { get; set; }
    }
}