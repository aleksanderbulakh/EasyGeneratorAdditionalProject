using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IContentRepository
    {
        IEnumerable<Content> GetAllContent();
        IEnumerable<Content> GetContentBySectionId(Guid id);
        Content GetContentById(Guid id);
        void CreateContent(Content contentModel);
        void EditContent(Content contentModel);
        void DeleteContent(Guid id);
    }
}
