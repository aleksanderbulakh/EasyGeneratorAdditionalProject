using EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Section : CourseSectionAndContentParentModel
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public virtual ICollection<Content> ContentCollection { get; set; }

        public Section()
        {
            Id = Guid.NewGuid();
            LastModifiedDate = DateTime.Now;
            ContentCollection = new List<Content>();
        }
    }
}
