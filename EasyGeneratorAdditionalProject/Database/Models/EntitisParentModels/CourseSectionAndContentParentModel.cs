using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Models.EntitisParentModels
{
    public class CourseSectionAndContentParentModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public void MarkAsModified()
        {
            LastModifiedDate = DateTime.Now;
        }

        public void SetDateFields()
        {
            CreatedOn = DateTime.Now;
            MarkAsModified();
        }

        public void SetId()
        {
            Id = Guid.NewGuid();
        }
    }
}