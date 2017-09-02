using System;
using System.Collections.Generic;

namespace HolidayRequestSystem.Domain.Utils
{
    public abstract class AggregateBase : IAggregate
    {
        protected AggregateBase()
        {
            this.UncommittedEvents = new List<IEvent>();
        }

        public IList<IEvent> UncommittedEvents { get; set; }
        public Guid Id { get; set; }

        protected void Publish(IEvent @event)
        {
            this.UncommittedEvents.Add(@event);
            ((dynamic)this).Apply((dynamic)@event);
        }
    }
}