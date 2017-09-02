using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HolidayRequestSystem.Domain.Utils;

namespace HolidayRequestSystem.Domain.Tests.Helpers
{
    /// <summary>
    /// Mock version of event store.
    /// </summary>
    public class TestEventStoreRepository : IEventStoreRepository
    {
        public List<IEvent> PublishedEvents { get; }
        public IList<IEvent> GivenEvents { get; }

        public TestEventStoreRepository()
        {
            this.PublishedEvents = new List<IEvent>();
            this.GivenEvents = new List<IEvent>();
        }

        public void AddGivenEvent(IEvent @event)
        {
            this.GivenEvents.Add(@event);
        }

        public T GetById<T>(Guid id) where T : IAggregate, new()
        {
            var aggregate = new T();

            foreach (var @event in this.GivenEvents)
            {
                ((dynamic)aggregate).Apply((dynamic)@event);
            }

            return aggregate;
        }

        public void Save<T>(T aggregate) where T : IAggregate
        {
            this.PublishedEvents.AddRange(aggregate.UncommittedEvents);
        }
    }
}