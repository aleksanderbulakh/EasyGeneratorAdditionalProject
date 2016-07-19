using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Section
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<Content> ContentCollection { get; set; }

        public Section()
        {
            ContentCollection = new List<Content>();
        }
    }
}