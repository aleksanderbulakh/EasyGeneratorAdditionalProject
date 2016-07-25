using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Repositories
{
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(IDatabaseContext context)
            : base(context) { }

        public List<Section> GetByCourseId(Guid id)
        {
            return Entity().Where(t => t.Course.Id == id).ToList();
        }
    }
}
