using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Models
{
    public class ContentModel
    {
        public Guid Id { get; set; }
        public string Tytle { get; set; }
        public string Type { get; set; }
        public List<object> Content { get; set; }
    }
}