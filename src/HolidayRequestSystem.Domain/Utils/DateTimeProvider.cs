using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayRequestSystem.Domain.Utils
{
    public static class DateTimeProvider
    {
        public static DateTime Now => GenerateDateTimeNow();

        public static Func<DateTime> GenerateDateTimeNow = () => DateTime.Now;
    }
}
