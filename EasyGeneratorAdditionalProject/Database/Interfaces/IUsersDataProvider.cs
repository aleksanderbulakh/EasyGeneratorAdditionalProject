using EasyGeneratorAdditionalProject.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IUsersDataProvider
    {
        IEnumerable<Users> GetAllUsers();

        Users GetUserById(Guid id);

        void CreateUser(Users userModel);

        void EditUser(Users userModel);

        void DeleteUser(Guid id);
    }
}
