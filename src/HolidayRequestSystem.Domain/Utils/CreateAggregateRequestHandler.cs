using MediatR;

namespace HolidayRequestSystem.Domain.Utils
{
    public abstract class CreateAggregateRequestHandler<TCommand, TAggregate> : IRequestHandler<TCommand>
        where TCommand : IRequest
        where TAggregate : IAggregate, new()
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        protected CreateAggregateRequestHandler(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }
        
        protected abstract void Handle(TAggregate aggregate, TCommand message);

        public void Handle(TCommand message)
        {
            var aggregate = new TAggregate();
            Handle(aggregate, message);
            _eventStoreRepository.Save(aggregate);
        }
    }
}