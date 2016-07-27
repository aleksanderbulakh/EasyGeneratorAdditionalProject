using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGeneratorAdditionalProject.Web.Convertors
{
    public interface IDateConvertor
    {
        long ConvertDateToMilliseconds(DateTime date);
    }
}
