using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();

        Role GetRoleById(Guid id);

        void CreateRole(Role roleModel);

        void EditRole(Role roleModel);

        void DeleteRole(Guid id);
    }
}
