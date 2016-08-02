﻿using System;
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
        public long CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public long LastModifiedDate { get; set; }
        public string Type { get; set; }
    }
}