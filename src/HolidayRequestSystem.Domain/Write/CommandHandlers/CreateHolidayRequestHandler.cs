using System;
using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Commands;
using HolidayRequestSystem.Domain.Write.Model;
using MediatR;

namespace HolidayRequestSystem.Domain.Write.CommandHandlers
{
    public class CreateHolidayRequestHandler : UpdateAggregateRequestHandler<CreateHolidayRequest, User>
    {
        public CreateHolidayRequestHandler(IEventStoreRepository eventStoreRepository) : base(eventStoreRepository)
        {
        }

        protected override Guid GetAggregateId(CreateHolidayRequest message)
        {
            return message.UserId;
        }

        protected override void Handle(User aggregate, CreateHolidayRequest message)
        {
            // TODO: superficial validations: start and end dates are required 
            // && end date is before start date
            // && leadId and project manager provided and exist

            aggregate.CreateHolidayRequest(message.StartDate, message.EndDate, message.LeaderId, message.ProjectManagerId);
        }
    }
}