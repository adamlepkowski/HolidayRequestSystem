using HolidayRequestSystem.Domain.Commands;
using HolidayRequestSystem.Domain.Domain;
using HolidayRequestSystem.Domain.Utils;
using MediatR;

namespace HolidayRequestSystem.Domain.CommandHandlers
{
    public class CreateHolidayRequestHandler : IRequestHandler<CreateHolidayRequest>
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public CreateHolidayRequestHandler(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Handle(CreateHolidayRequest message)
        {
            var user = _eventStoreRepository.GetById<User>(message.UserId);

            user.CreateHolidayRequest(message.StartDate, message.EndDate);

            _eventStoreRepository.Save(user);
        }
    }
}