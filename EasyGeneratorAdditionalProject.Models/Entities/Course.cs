using EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Course : CourseSectionAndContentParentModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Section> SectionsList { get; set; }

        public Course()
        {
            SetId();
            SectionsList = new List<Section>();
        }

        public Course(string title, string description, Guid userId, string userName)
        {
            ThrowIfTileInvalid(title);
            ThrowIfDescriptionInvalid(description);

            SetId();
            Title = title;
            Description = description;
            UserId = userId;
            CreatedBy = userName;
            SetDateFields();
        }

        #region Update
        public void UpdateTitle(string title)
        {
            ThrowIfTileInvalid(title);

            Title = title;
            MarkAsModified();
        }

        public void UpdateDescription(string description)
        {
            ThrowIfDescriptionInvalid(description);

            Description = description;
            MarkAsModified();
        }
        #endregion

        #region Validation
        private void ThrowIfTileInvalid(string title)
        {
            if (title == null || title.Length > 225)
                throw new ArgumentException("Invalid title");
        }

        private void ThrowIfDescriptionInvalid(string description)
        {
            if (description == null)
                throw new ArgumentException("Invalid Description");
        }
        #endregion
    }
}
