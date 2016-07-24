using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels
{
    public class CourseSectionAndContentParentModel
    {
        public CourseSectionAndContentParentModel()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateToMiliseconds(DateTime.UtcNow);
            MarkAsModified();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public long CreatedOn { get; set; }
        public long LastModifiedDate { get; set; }

        public void MarkAsModified()
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
