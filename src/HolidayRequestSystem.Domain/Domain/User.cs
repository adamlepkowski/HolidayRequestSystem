using System;
using System.Collections.Generic;
using HolidayRequestSystem.Domain.Events;
using HolidayRequestSystem.Domain.Utils;

namespace HolidayRequestSystem.Domain.Domain
{
    public class User : IAggregate
    {
        public IList<IEvent> UncommittedEvents { get; set; }
        public Guid Id { get; set; }

        public User()
        {
            this.UncommittedEvents = new List<IEvent>();
            this.Id = Guid.Empty;
        }

        public void CreateHolidayRequest(DateTime startDate, DateTime endDate)
        {
            Publish(new HolidayRequestCreated(Guid.Empty, startDate, endDate));
        }

        public void Apply(HolidayRequestCreated @event)
        {

        }

        private void Publish(IEvent @event)
        {
            this.UncommittedEvents.Add(@event);
            ((dynamic)this).Apply((dynamic)@event);
        }
    }
}