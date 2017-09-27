using System;
using HolidayRequestSystem.Domain.Utils;

namespace HolidayRequestSystem.Domain.Common.Events
{
    [EventTypeId("24c1e8a3-b294-4c4c-91e6-7703e5808712")]
    public class HolidayRequestCanceled : IEvent
    {
        public Guid Id { get; set; }

        public DateTime SubmittedAt { get; set; }

        public HolidayRequestCanceled(Guid id, DateTime submittedAt)
        {
            this.Id = id;
            this.SubmittedAt = submittedAt;
        }
    }
}