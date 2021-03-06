﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Course : Entity
    {
        public virtual User User { get; protected internal set; }
        public string Description { get; protected internal set; }

        public virtual ICollection<Section> SectionsList { get; protected internal set; }

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
        public void UpdateDescription(string description, string userName)
        {
            ThrowIfDescriptionInvalid(description);
            ThrowIfUserNameInvalid(userName);

            Description = description;
            MarkAsModified(userName);
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
