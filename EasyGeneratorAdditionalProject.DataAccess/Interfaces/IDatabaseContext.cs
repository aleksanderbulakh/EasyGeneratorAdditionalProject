using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.DataAccess.Interfaces
{
    public interface IDatabaseContext
    {
        IDbSet<T> GetSet<T>() where T : class;
    }
}
