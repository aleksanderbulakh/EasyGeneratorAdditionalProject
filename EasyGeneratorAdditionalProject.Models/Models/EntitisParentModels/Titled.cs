using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels
{
    public class Titled : Identity
    {
        public Titled()
            :base()
        {
            CreatedOn = DateToMiliseconds(DateTime.UtcNow);
            MarkAsModified();
        }
        
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public long CreatedOn { get; set; }
        public long LastModifiedDate { get; set; }

        protected void ThrowIfTileInvalid(string title)
        {
            if (title == null || title.Length == 0 || title.Length > 225)
                throw new ArgumentException("Invalid title");
        }

        protected void MarkAsModified()
        {
            LastModifiedDate = DateToMiliseconds(DateTime.UtcNow);
        }

        private long DateToMiliseconds(DateTime date)
        {
            DateTime minDate = new DateTime(1969, 12, 31, 0, 0, 0);

            return (date - minDate).Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
