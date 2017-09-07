using System;
using HolidayRequestSystem.Domain.Utils;

namespace HolidayRequestSystem.Domain.Events
{
    [EventTypeId("AC0F8448-7A51-4121-97CD-C436F15750DC")]
    public class HolidayRequestCreated : IEvent
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid LeaderId { get; set; }
        public Guid ProjectManagerId { get; set; }
        public DateTime SubmittedAt { get; set; }

        public HolidayRequestCreated(Guid id, DateTime startDate, DateTime endDate, Guid leaderId, Guid projectManagerId, DateTime submittedAt)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.LeaderId = leaderId;
            this.ProjectManagerId = projectManagerId;
            this.SubmittedAt = submittedAt;
        }
    }
}