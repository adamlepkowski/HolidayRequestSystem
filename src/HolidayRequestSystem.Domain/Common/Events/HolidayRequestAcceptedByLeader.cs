using System;
using HolidayRequestSystem.Domain.Utils;

namespace HolidayRequestSystem.Domain.Common.Events
{
    [EventTypeId("25861a68-d483-4194-89a0-f6cfec47bb9a")]
    public class HolidayRequestAcceptedByLeader : IEvent
    {
        public Guid HolidayRequestId { get; set; }
        public Guid AccepterId { get; set; }
        public DateTime SubmittedAt { get; set; }

        public HolidayRequestAcceptedByLeader(Guid holidayRequestId, Guid accepterId, DateTime submittedAt)
        {
            this.HolidayRequestId = holidayRequestId;
            this.AccepterId = accepterId;
            this.SubmittedAt = submittedAt;
        }
    }
}