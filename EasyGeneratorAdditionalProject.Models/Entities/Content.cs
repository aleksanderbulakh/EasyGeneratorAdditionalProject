using EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Content : Titled
    {
        public Section Section { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Material> MaterialsCollection { get; set; }
        public virtual ICollection<SingleSelectAnswer> SingleSelectAnswerCollection { get; set; }
        public virtual ICollection<MultipleSelectAnswer> MultipleSelectAnswerCollection { get; set; }
        public virtual ICollection<SingleSelectImageAnswer> SingleSelectImageAnswerCollection { get; set; }

        public Content()
            : base()
        {
            MaterialsCollection = new List<Material>();
            SingleSelectAnswerCollection = new List<SingleSelectAnswer>();
            MultipleSelectAnswerCollection = new List<MultipleSelectAnswer>();
            SingleSelectImageAnswerCollection = new List<SingleSelectImageAnswer>();
        }

        public Content(string title, string type, Section section):
            this()
        {
            ThrowIfTileInvalid(title);
            ThrowIfTypeInvalid(type);

            Title = title;
            Type = type;
            Section = section;
            CreatedBy = section.CreatedBy;
        }

        private void ThrowIfTypeInvalid(string type)
        {
            if (type != "material" || type != "single" || type != "multiple" || type != "single_image")
                throw new ArgumentException("Type is not valid.");
        }
    }
}
