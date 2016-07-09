using EasyGeneratorAdditionalProject.Database.Context;
using EasyGeneratorAdditionalProject.Database.Entities;
using EasyGeneratorAdditionalProject.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Database.Providers
{
    public class UsersDataProvider : IUsersDataProvider
    {
        private DatabaseContext _context;
        public UsersDataProvider(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Users;
        }

        public Users GetUserById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public void CreateUser(Users userModel)
        {
            _context.Users.Add(userModel);
            _context.SaveChanges();
        }

        public void EditUser(Users userModel)
        {
            _context.Entry(userModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            _context.Users.Remove(_context.Users.Find(id));
            _context.SaveChanges();
        }
    }
}