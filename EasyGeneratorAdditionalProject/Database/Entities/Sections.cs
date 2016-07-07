using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Sections
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Courses Course { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime CreatedBy { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }

        public Sections()
        {
            Questions = new List<Questions>();
        }
    }
}