using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Courses
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual ICollection<Sections> SectionsList { get; set; }

        public Courses()
        {
            SectionsList = new List<Sections>();
        }
    }
}