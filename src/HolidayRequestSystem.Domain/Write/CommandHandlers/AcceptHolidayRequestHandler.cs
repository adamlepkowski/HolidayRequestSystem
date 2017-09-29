using System;
using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Commands;
using HolidayRequestSystem.Domain.Write.Model;

namespace HolidayRequestSystem.Domain.Write.CommandHandlers
{
    public class AcceptHolidayRequestHandler : UpdateAggregateRequestHandler<AcceptHolidayRequest, User>
    {
        public AcceptHolidayRequestHandler(IEventStoreRepository eventStoreRepository) : base(eventStoreRepository)
        {
        }

        protected override Guid GetAggregateId(AcceptHolidayRequest message)
        {
            return message.UserId;
        }

        protected override void Handle(User aggregate, AcceptHolidayRequest message)
        {
            aggregate.AcceptHolidayRequest(message.HolidayRequestId, message.AccepterId);
        }
    }
}