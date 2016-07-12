using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Models
{
    public class CourseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public List<SectionModel> SectionsList { get; set; }
    }
}