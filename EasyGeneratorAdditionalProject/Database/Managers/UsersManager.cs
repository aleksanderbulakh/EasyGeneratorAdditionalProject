using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Managers
{
    public class UsersManager : IDisposable
    {
        private readonly IUsersDataProvider _repository;
        public UsersManager(IUsersDataProvider repository)
        {
            _repository = repository;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public Users GetUserById(Guid id)
        {
            return _repository.GetUserById(id);
        }

        public void CreateUser(Users userModel)
        {
            if (userModel.Id == null)
                userModel.Id = Guid.NewGuid();

            _repository.CreateUser(userModel);
        }

        public void EditUser(Users userModel)
        {
            _repository.EditUser(userModel);
        }

        public void DeleteUser(Guid id)
        {
            _repository.DeleteUser(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}