﻿using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IContentDataProvider
    {
        IEnumerable<Content> GetAllContent();

        void CreateContent(Content contentModel);

        IEnumerable<Content> GetContentBySectionId(Guid id);

        Content GetContentById(Guid id);

        void EditContent(Content contentModel);

        void DeleteContent(Guid id);
    }
}
