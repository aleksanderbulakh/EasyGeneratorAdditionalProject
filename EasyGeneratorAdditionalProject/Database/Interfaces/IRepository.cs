using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Database.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Create(T modelObj);
        List<T> GetByUserId(Guid id);
        T GetById(Guid id);
        void Edit(T modelObj);
        bool Delete(Guid id);
    }
}
