using System;
using HolidayRequestSystem.Domain.Utils;

namespace HolidayRequestSystem.Domain.Common.Events
{
    [EventTypeId("391e894e-fd33-429b-b32f-882c3b03d9f9")]
    public class HolidayRequestAcceptedByProjectManager : IEvent
    {
        public Guid HolidayRequestId { get; set; }
        public Guid AccepterId { get; set; }
        public DateTime SubmittedAt { get; set; }

        public HolidayRequestAcceptedByProjectManager(Guid holidayRequestId, Guid accepterId, DateTime submittedAt)
        {
            this.HolidayRequestId = holidayRequestId;
            this.AccepterId = accepterId;
            this.SubmittedAt = submittedAt;
        }
    }
}