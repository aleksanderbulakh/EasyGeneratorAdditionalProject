using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T modelObj);
        T GetById(Guid id);
        void Delete(T modelObj);
    }
}
