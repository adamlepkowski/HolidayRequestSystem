using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Commands;
using HolidayRequestSystem.Domain.Write.Model;
using MediatR;

namespace HolidayRequestSystem.Domain.Write.CommandHandlers
{
    public class CancelHolidayRequestHandler : IRequestHandler<CancelHolidayRequest>
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public CancelHolidayRequestHandler(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Handle(CancelHolidayRequest message)
        {
            var user = this._eventStoreRepository.GetById<User>(message.UserId);

            user.CancelHolidayRequest(message.HolidayRequestId);

            _eventStoreRepository.Save(user);
        }
    }
}