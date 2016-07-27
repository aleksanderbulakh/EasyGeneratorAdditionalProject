using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGeneratorAdditionalProject.Web.Convertors
{
    public class DateConvertor : IDateConvertor
    {
        private readonly DateTime minDate;
        public DateConvertor()
        {
            minDate = new DateTime(1969, 12, 31, 23, 59, 59);
        }

        public long ConvertDateToMilliseconds(DateTime date)
        {
            return (date - minDate).Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}