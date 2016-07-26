using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Section : Titled
    {
        public Course Course { get; set; }

        public virtual ICollection<Content> ContentCollection { get; set; }

        public Section()
            : base()
        {
            ContentCollection = new List<Content>();
        }

        public Section(string title, Course course)
            :this()
        {
            ThrowIfTileInvalid(title);
            ThrowIfCourseInvalid(course);

            Title = title;
            Course = course;
            CreatedBy = course.CreatedBy;
        }

        public void UpdateTitle(string title)
        {
            ThrowIfTileInvalid(title);

            Title = title;
            MarkAsModified();
        }

        private void ThrowIfCourseInvalid(Course course)
        {
            if (course == null)
                throw new ArgumentException("Course is not found.");
        }
    }
}
