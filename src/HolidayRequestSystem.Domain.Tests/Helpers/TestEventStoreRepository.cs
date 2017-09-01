using System;
using System.Collections.Generic;
using HolidayRequestSystem.Domain.Utils;

namespace HolidayRequestSystem.Domain.Tests.Helpers
{
    /// <summary>
    /// Mock version of event store.
    /// </summary>
    public class TestEventStoreRepository : IEventStoreRepository
    {
        public List<IEvent> PublishedEvents { get; }

        public TestEventStoreRepository()
        {
            this.PublishedEvents = new List<IEvent>();
        }

        public T GetById<T>(Guid id) where T : IAggregate, new()
        {
            // TODO: get events
            
            var aggregate = new T();

            // TODO: apply retrieved events
            
            return aggregate;
        }

        public void Save<T>(T aggregate) where T : IAggregate
        {
            this.PublishedEvents.AddRange(aggregate.UncommittedEvents);
        }
    }
}