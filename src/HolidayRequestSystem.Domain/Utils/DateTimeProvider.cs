using System;

namespace HolidayRequestSystem.Domain.Utils
{
    public static class DateTimeProvider
    {
        public static DateTime Now => GenerateDateTimeNow();

        public static Func<DateTime> GenerateDateTimeNow = () => DateTime.Now;
    }
}
