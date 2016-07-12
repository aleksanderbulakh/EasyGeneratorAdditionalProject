using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Models
{
    public class SectionModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public List<object> ContentList { get; set; }
    }
}