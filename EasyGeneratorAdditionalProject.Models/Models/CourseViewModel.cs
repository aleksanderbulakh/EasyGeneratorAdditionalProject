using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Models
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public List<object> SectionsList { get; set; }
    }
}
