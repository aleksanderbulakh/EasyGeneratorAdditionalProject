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

        public Section(string title, string userName, Course course)
            :this()
        {
            ThrowIfTileInvalid(title);
            ThrowIfCourseInvalid(course);
            ThrowIfUserNameInvalid(userName);

            Title = title;
            Course = course;
            CreatedBy = course.CreatedBy;
            ModifiedBy = userName;
        }

        public void UpdateTitle(string title, string userName)
        {
            ThrowIfTileInvalid(title);
            ThrowIfUserNameInvalid(userName);

            Title = title;
            ModifiedBy = userName;
            MarkAsModified();
        }

        private void ThrowIfCourseInvalid(Course course)
        {
            if (course == null)
                throw new ArgumentException("Course is not found.");
        }
    }
}
