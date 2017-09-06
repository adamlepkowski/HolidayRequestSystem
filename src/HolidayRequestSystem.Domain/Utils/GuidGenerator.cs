using System;

namespace HolidayRequestSystem.Domain.Utils
{
    public static class GuidGenerator
    {
        public static Guid NewGuid()
        {
            return GenerateGuid();
        }

        public static Func<Guid> GenerateGuid = () => Guid.NewGuid();
    }
}
