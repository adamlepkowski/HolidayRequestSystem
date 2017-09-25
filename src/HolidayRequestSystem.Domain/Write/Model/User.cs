using System;
using System.Collections.Generic;
using System.Linq;
using HolidayRequestSystem.Domain.Common.Events;
using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Model.Exceptions;

namespace HolidayRequestSystem.Domain.Write.Model
{
    public class User : AggregateBase
    {
        public IList<HolidayRequest> HolidayRequest { get; set; }

        public User()
        {
            this.Id = Guid.Empty; // temporary
            this.HolidayRequest = new List<HolidayRequest>();
        }

        public void CreateHolidayRequest(DateTime startDate, DateTime endDate, Guid leaderId, Guid projectManagerId)
        {
            if (IsInRange(this.HolidayRequest, startDate, endDate))
            {
                throw new HolidayRequestAlreadyExistsForSpecifiedDateRange();
            }

            Publish(new HolidayRequestCreated(GuidGenerator.NewGuid(), startDate, endDate, leaderId, projectManagerId, DateTimeProvider.Now));
        }

        private static bool IsInRange(IList<HolidayRequest> holidayRequests, DateTime startDate, DateTime endDate)
        {
            return holidayRequests.Any(m =>
                m.StartDate < startDate && startDate < m.EndDate
                ||
                m.StartDate < endDate && endDate <= m.EndDate
                ||
                startDate < m.StartDate && m.StartDate < endDate
                ||
                startDate < m.EndDate && m.EndDate <= endDate);
        }

        public void Apply(HolidayRequestCreated @event)
        {
            this.HolidayRequest.Add(new HolidayRequest(@event.Id, @event.StartDate, @event.EndDate));
        }
    }
}