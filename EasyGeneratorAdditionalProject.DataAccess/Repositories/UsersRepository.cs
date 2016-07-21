using EasyGeneratorAdditionalProject.DataAccess.Context;
using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using EasyGeneratorAdditionalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private IDatabaseContext _context;
        public UsersRepository(IDatabaseContext context)
        {
            _context = context;
        }

        private IDbSet<User> Entity()
        {
            return _context.GetSet<User>();
        }

        public List<User> GetAll()
        {
            return Entity().ToList();
        }

        public User GetById(Guid id)
        {
            return Entity().Find(id);
        }

        public void Create(User modelObj)
        {
            Entity().Add(modelObj);
        }

        public void Delete(Guid id)
        {
            Entity().Remove(_context.GetSet<User>().Find(id));
        }

        public List<User> GetByForeignId(Guid id)
        {
            return Entity().Where(t => t.Id == id).ToList();
        }
    }
}
