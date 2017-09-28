using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Commands;
using HolidayRequestSystem.Domain.Write.Model;
using MediatR;

namespace HolidayRequestSystem.Domain.Write.CommandHandlers
{
    public class AcceptHolidayRequestHandler : IRequestHandler<AcceptHolidayRequest>
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public AcceptHolidayRequestHandler(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Handle(AcceptHolidayRequest message)
        {
            var user = this._eventStoreRepository.GetById<User>(message.UserId);

            user.AcceptHolidayRequest(message.HolidayRequestId, message.AccepterId);

            _eventStoreRepository.Save(user);
        }
    }
}