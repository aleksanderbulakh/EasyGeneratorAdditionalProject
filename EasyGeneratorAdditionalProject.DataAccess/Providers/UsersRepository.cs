using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Providers
{
    public class UsersRepository : IRepository<User>
    {
        private DatabaseContext _context;
        public UsersRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User modelObj)
        {
            _context.Users.Add(modelObj);
            _context.SaveChanges();

            return GetById(modelObj.Id);
        }

        public void Edit(User modelObj)
        {
            _context.Entry(modelObj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            _context.Users.Remove(_context.Users.Find(id));
            _context.SaveChanges();

            if (GetById(id) == null)
                return true;
            else
                return false;
        }

        public List<User> GetByForeignId(Guid id)
        {
            return _context.Users.Where(t => t.Id == id).ToList();
        }
        
    }
}
