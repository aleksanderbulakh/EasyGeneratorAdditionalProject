using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Content
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public Sections Section { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Materials> MaterialsCollection { get; set; }
        public virtual ICollection<SingleSelectQuestions> SingleSelectQuestionsCollection { get; set; }
        public virtual ICollection<MultipleSelectQuestion> MultipleSelectQuestionCollection { get; set; }
        public virtual ICollection<SingleSelectImageQuestions> SingleSelectImageQuestionCollection { get; set; }

        public Content()
        {
            MaterialsCollection = new List<Materials>();
            SingleSelectQuestionsCollection = new List<SingleSelectQuestions>();
            MultipleSelectQuestionCollection = new List<MultipleSelectQuestion>();
            SingleSelectImageQuestionCollection = new List<SingleSelectImageQuestions>();
        }
    }
}