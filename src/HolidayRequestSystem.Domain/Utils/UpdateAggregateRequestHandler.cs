using System;
using MediatR;

namespace HolidayRequestSystem.Domain.Utils
{
    public abstract class UpdateAggregateRequestHandler<TCommand, TAggregate> : IRequestHandler<TCommand>
        where TCommand : IRequest
        where TAggregate : IAggregate, new()
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        protected UpdateAggregateRequestHandler(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        // in the future, this value can be retrieved by convention based on an attribute over a property
        protected abstract Guid GetAggregateId(TCommand message);

        protected abstract void Handle(TAggregate aggregate, TCommand message);

        public void Handle(TCommand message)
        {
            var aggregate = _eventStoreRepository.GetById<TAggregate>(this.GetAggregateId(message));
            Handle(aggregate, message);
            _eventStoreRepository.Save(aggregate);
        }
    }
}