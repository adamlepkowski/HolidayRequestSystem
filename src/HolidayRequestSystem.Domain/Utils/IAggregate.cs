using System;
using System.Collections.Generic;

namespace HolidayRequestSystem.Domain.Utils
{
    public interface IAggregate
    {
        Guid Id { get; set; }
        IList<IEvent> UncommittedEvents { get; set; }
    }
}