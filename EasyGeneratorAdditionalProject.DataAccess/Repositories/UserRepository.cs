using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDatabaseContext context)
            : base(context) { }

        public List<User> GetByRoleId(Guid id)
        {
            return Entity().Where(t => t.Role.Id == id).ToList();
        }
    }
}
