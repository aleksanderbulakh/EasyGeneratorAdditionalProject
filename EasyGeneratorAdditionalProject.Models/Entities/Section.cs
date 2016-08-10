using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Section : Entity
    {
        public virtual Course Course { get; protected internal set; }

        public virtual ICollection<Question> QuestionCollection { get; protected internal set; }

        public Section()
            : base()
        {
            QuestionCollection = new List<Question>();
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

        private void ThrowIfCourseInvalid(Course course)
        {
            if (course == null)
                throw new ArgumentException("Course is not found.");
        }
    }
}
