using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HolidayRequestSystem.Domain.Read.Model
{
    public class HolidayRequest
    {
        public Guid HolidayRequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid LeaderId { get; set; }
        public Guid ProjectManagerId { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}