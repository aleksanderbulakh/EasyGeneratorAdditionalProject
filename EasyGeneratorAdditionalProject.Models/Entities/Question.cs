using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Question : Titled
    {
        public Section Section { get; set; }
        public string Type { get; set; }
        
        public virtual ICollection<QuestionAnswer> AnswersCollection { get; set; }

        public Question()
            : base()
        {
            AnswersCollection = new List<QuestionAnswer>();
        }

        public Question(string title, string type, Section section):
            this()
        {
            ThrowIfTileInvalid(title);
            ThrowIfTypeInvalid(type);

            Title = title;
            Type = type;
            Section = section;
            CreatedBy = ModifiedBy = section.CreatedBy;
        }

        public void UpdateTitle(string title, string userName)
        {
            ThrowIfTileInvalid(title);
            ThrowIfUserNameInvalid(userName);

            Title = title;
            ModifiedBy = userName;
            MarkAsModified();

        }

        private void ThrowIfTypeInvalid(string type)
        {
            if (type != "material" || type != "single" || type != "multiple" || type != "single_image")
                throw new ArgumentException("Type is not valid.");
        }
    }
}
