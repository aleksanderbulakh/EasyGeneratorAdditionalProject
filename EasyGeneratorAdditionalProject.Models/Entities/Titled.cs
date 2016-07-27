using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Titled : Identity
    {
        public Titled()
            : base()
        {
            CreatedOn = LastModifiedDate = DateTime.UtcNow;
        }

        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        protected void ThrowIfTileInvalid(string title)
        {
            if (title == null || title.Length == 0 || title.Length > 225)
                throw new ArgumentException("Invalid title");
        }

        protected void ThrowIfUserNameInvalid(string userName)
        {
            if (userName == null || userName.Length == 0 || userName.Length > 255)
                throw new ArgumentException("Invalid user name.");
        }

        protected void MarkAsModified()
        {
            LastModifiedDate = DateTime.UtcNow;
        }
    }
}
