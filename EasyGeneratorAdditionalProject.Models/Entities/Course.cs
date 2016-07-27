using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Course : Titled
    {
        public User User { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Section> SectionsList { get; set; }

        public Course() 
                :base()
        {
            SectionsList = new List<Section>();
        }

        public Course(string title, string description, User user):this()
        {
            ThrowIfTileInvalid(title);
            ThrowIfDescriptionInvalid(description);
            ThrowIfUserInvalid(user);

            Title = title;
            Description = description;
            User = user;
            CreatedBy = ModifiedBy = user.UserName;            
        }

        #region Update
        public void UpdateTitle(string title, string userName)
        {
            ThrowIfTileInvalid(title);
            ThrowIfUserNameInvalid(userName);

            Title = title;
            ModifiedBy = userName;
            MarkAsModified();
        }

        public void UpdateDescription(string description, string userName)
        {
            ThrowIfDescriptionInvalid(description);
            ThrowIfUserNameInvalid(userName);

            Description = description;
            ModifiedBy = userName;
            MarkAsModified();
        }
        #endregion

        #region Validation
        private void ThrowIfDescriptionInvalid(string description)
        {
            if (description == null)
                throw new ArgumentException("Invalid Description");
        }

        private void ThrowIfUserInvalid(User user)
        {
            if (user == null)
                throw new ArgumentException("User is not found.");
        }
        #endregion
    }
}
