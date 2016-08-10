using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Entity : Identifier
    {
        public Entity()
            : base()
        {
            CreatedOn = LastModifiedDate = DateTime.UtcNow;
        }

        public string Title { get; protected internal set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        protected void ThrowIfTileInvalid(string title)
        {
            if (title == null || title.Length == 0 || title.Length > 225)
                throw new ArgumentException("Invalid title");
        }

        public void UpdateTitle(string title, string userName)
        {
            ThrowIfTileInvalid(title);
            ThrowIfUserNameInvalid(userName);

            Title = title;
            MarkAsModified(userName);
        }

        protected void ThrowIfUserNameInvalid(string userName)
        {
            if (userName == null || userName.Length == 0 || userName.Length > 255)
                throw new ArgumentException("Invalid user name.");
        }

        public void MarkAsModified(string userName)
        {
            ModifiedBy = userName;
            LastModifiedDate = DateTime.UtcNow;
        }
    }
}
