using System;

namespace HolidayRequestSystem.Domain.Write.Model.Exceptions
{
    public class EntityNotExists : Exception
    {
        public readonly Guid HolidayRequestId;
        public readonly Type Type;

        public EntityNotExists(Type type, Guid holidayRequestId)
        {
            this.Type = type;
            this.HolidayRequestId = holidayRequestId;
        }
    }
}