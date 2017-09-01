using System;

namespace HolidayRequestSystem.Domain.Utils
{
    // TODO: move it somewhere else when data access implemented
    public interface IEventStoreRepository
    {
        T GetById<T>(Guid id) where T : IAggregate, new();
        void Save<T>(T aggregate) where T : IAggregate;
    }
}