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
        public IEnumerable<Users> GetAllUsers()
        {
            using (var db = new DatabaseContext())
            {
                return db.Users;
            }
        }

        public Users GetUserById(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                return db.Users.Find(id);
            }
        }

        public void CreateUser(Users userModel)
        {
            using (var db = new DatabaseContext())
            {
                db.Users.Add(userModel);
                db.SaveChanges();
            }
        }

        public void EditUser(Users userModel)
        {
            using (var db = new DatabaseContext())
            {
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteUser(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                db.Users.Remove(db.Users.Find(id));
                db.SaveChanges();
            }

        }
    }
}