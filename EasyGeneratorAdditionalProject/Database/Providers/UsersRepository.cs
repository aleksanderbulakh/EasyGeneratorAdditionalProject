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
    public class UsersRepository : IUserRepository
    {
        private DatabaseContext _context;
        public UsersRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUserById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public void CreateUser(User modelObj)
        {
            _context.Users.Add(modelObj);
            _context.SaveChanges();
        }

        public void EditUser(User modelObj)
        {
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            _context.Users.Remove(_context.Users.Find(id));
            _context.SaveChanges();
        }

        public User GetFirstUser()
        {
            return _context.Users.First();
        }
    }
}