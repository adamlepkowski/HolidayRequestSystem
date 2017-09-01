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

        public HolidayRequestCreated(Guid guid, DateTime startDate, DateTime endDate)
        {
            this.Id = guid;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}