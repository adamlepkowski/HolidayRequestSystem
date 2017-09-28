using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Commands;
using HolidayRequestSystem.Domain.Write.Model;
using MediatR;

namespace HolidayRequestSystem.Domain.Write.CommandHandlers
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
            // TODO: superficial validations: start and end dates are required 
            // && end date is before start date
            // && leadId and project manager provided and exist

            var user = _eventStoreRepository.GetById<User>(message.UserId);

            user.CreateHolidayRequest(message.StartDate, message.EndDate, message.LeaderId, message.ProjectManagerId);

            _eventStoreRepository.Save(user);
        }
    }
}