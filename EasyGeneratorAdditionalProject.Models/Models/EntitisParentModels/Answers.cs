using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Models.Models.EntitisParentModels
{
    public class Answers : Identity
    {
        public Content Content { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Answers() 
            : base() { }
    }
}
