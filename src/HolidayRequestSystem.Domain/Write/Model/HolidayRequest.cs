using System;

namespace HolidayRequestSystem.Domain.Write.Model
{
    public class HolidayRequest // TODO: this will be a seperate aggregate root
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid LeaderId { get; set; }
        public Guid ProjectManagerId { get; set; }
        public bool AcceptedByProjectManager { get; set; }

        public HolidayRequest(Guid id, DateTime startDate, DateTime endDate, Guid leaderId, Guid projectManagerId)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.LeaderId = leaderId;
            this.ProjectManagerId = projectManagerId;
        }
    }
}