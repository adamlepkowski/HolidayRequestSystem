using System;
using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Commands;
using HolidayRequestSystem.Domain.Write.Model;
using MediatR;

namespace HolidayRequestSystem.Domain.Write.CommandHandlers
{
    public class CancelHolidayRequestHandler : UpdateAggregateRequestHandler<CancelHolidayRequest, User>
    {
        public CancelHolidayRequestHandler(IEventStoreRepository eventStoreRepository) : base(eventStoreRepository)
        {
        }

        protected override Guid GetAggregateId(CancelHolidayRequest message)
        {
            return message.UserId;
        }

        protected override void Handle(User aggregate, CancelHolidayRequest message)
        {
            aggregate.CancelHolidayRequest(message.HolidayRequestId);
        }
    }
}