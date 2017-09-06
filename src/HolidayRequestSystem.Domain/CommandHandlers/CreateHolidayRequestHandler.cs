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
            // TODO: superficial validations: start and end dates are required && end date is before start date

            var user = _eventStoreRepository.GetById<User>(message.UserId); // move it to base class when created

            user.CreateHolidayRequest(message.StartDate, message.EndDate);

            _eventStoreRepository.Save(user); // move it to base class when created
        }
    }
}