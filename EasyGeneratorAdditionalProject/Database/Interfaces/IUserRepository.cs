using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(Guid id);

        void CreateUser(User userModel);

        void EditUser(User userModel);

        void DeleteUser(Guid id);
    }
}
