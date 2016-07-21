using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Create(T modelObj);
        List<T> GetByForeignId(Guid id);
        T GetById(Guid id);
        void Edit(T modelObj);
        bool Delete(Guid id);
    }
}
