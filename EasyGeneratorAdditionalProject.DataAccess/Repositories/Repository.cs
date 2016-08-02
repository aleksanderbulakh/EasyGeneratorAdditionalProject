using EasyGeneratorAdditionalProject.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IDatabaseContext _context;
        public Repository(IDatabaseContext context)
        {
            _context = context;
        }

        protected IDbSet<T> Entity()
        {
            return _context.GetSet<T>();
        }

        public void Add(T modelObj)
        {
            Entity().Add(modelObj);
        }

        public void Delete(T modelObj)
        {
            Entity().Remove(modelObj);
        }

        public T GetById(Guid id)
        {
            return Entity().Find(id);
        }
    }
}
