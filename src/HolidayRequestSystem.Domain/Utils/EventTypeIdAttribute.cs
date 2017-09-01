using System;

namespace HolidayRequestSystem.Domain.Utils
{
    /// <summary>
    /// Copied from scooletz event sourcing repository.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EventTypeIdAttribute : Attribute
    {
        public readonly Guid Id;

        public EventTypeIdAttribute(string id)
        {
            Id = new Guid(id);
        }
    }
}