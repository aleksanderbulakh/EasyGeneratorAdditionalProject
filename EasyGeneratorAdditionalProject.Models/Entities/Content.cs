﻿using EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Entities
{
    public class Content : CourseSectionAndContentParentModel
    {
        public Section Section { get; set; }
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
