using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Web.ViewModels
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Type { get; set; }
    }
}