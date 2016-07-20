using EasyGeneratorAdditionalProject.Database.Models.EntitisParentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Course : CourseSectionAndContentParentModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Section> SectionsList { get; set; }

        public Course()
        {
            Id = Guid.NewGuid();
            LastModifiedDate = DateTime.Now;
            SectionsList = new List<Section>();
        }
    }
}