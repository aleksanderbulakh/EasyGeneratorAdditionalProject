using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Managers
{
    public class RolesManager : IDisposable
    {
        private readonly IRolesDataProvider _repository;
        public RolesManager(IRolesDataProvider repository)
        {
            _repository = repository;
        }

        public IEnumerable<Roles> GetAllRoles()
        {
            return _repository.GetAllRoles();
        }

        public Roles GetRoleById(Guid id)
        {
            return _repository.GetRoleById(id);
        }

        public void CreateRole(Roles roleModel)
        {
            if (roleModel.Id == null)
                roleModel.Id = Guid.NewGuid();

            _repository.CreateRole(roleModel);
        }

        public void EditRole(Roles roleModel)
        {
            _repository.EditRole(roleModel);
        }

        public void DeleteRole(Guid id)
        {
            _repository.DeleteRole(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}