using HolidayRequestSystem.Domain.Common.Events;
using HolidayRequestSystem.Domain.Read.Model;
using HolidayRequestSystem.Domain.Utils;
using MediatR;

namespace HolidayRequestSystem.Domain.Read.EventHandlers
{
    public class HolidayRequestCreatedHandler : INotificationHandler<HolidayRequestCreated>
    {
        private readonly IGenericRepository<HolidayRequest> _holidayRequest;

        public HolidayRequestCreatedHandler(IGenericRepository<HolidayRequest> holidayRequest)
        {
            _holidayRequest = holidayRequest;
        }

        public void Handle(HolidayRequestCreated notification)
        {
            _holidayRequest.Add(
                new HolidayRequest
                {
                    HolidayRequestId = notification.Id,
                    StartDate = notification.StartDate,
                    EndDate = notification.EndDate,
                    LeaderId = notification.LeaderId,
                    ProjectManagerId = notification.ProjectManagerId,
                    SubmittedAt = notification.SubmittedAt
                });
        }
    }
}