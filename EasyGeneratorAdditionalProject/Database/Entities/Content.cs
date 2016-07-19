﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Entities
{
    public class Content
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public Section Section { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Material> MaterialsCollection { get; set; }
        public virtual ICollection<SingleSelectAnswer> SingleSelectAnswerCollection { get; set; }
        public virtual ICollection<MultipleSelectAnswer> MultipleSelectAnswerCollection { get; set; }
        public virtual ICollection<SingleSelectImageAnswer> SingleSelectImageAnswerCollection { get; set; }

        public Content()
        {
            MaterialsCollection = new List<Material>();
            SingleSelectAnswerCollection = new List<SingleSelectAnswer>();
            MultipleSelectAnswerCollection = new List<MultipleSelectAnswer>();
            SingleSelectImageAnswerCollection = new List<SingleSelectImageAnswer>();
        }
    }
}