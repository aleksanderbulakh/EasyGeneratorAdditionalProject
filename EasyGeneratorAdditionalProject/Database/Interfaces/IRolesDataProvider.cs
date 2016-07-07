using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IRolesDataProvider
    {
        IEnumerable<Roles> GetAllRoles();

        Roles GetRoleById(Guid id);

        void CreateRole(Roles roleModel);

        void EditRole(Roles roleModel);

        void DeleteRole(Guid id);
    }
}
