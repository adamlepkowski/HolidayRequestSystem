using System;

namespace HolidayRequestSystem.Domain.Domain
{
    public class HolidayRequest // TODO: this will be a seperate aggregate root
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public HolidayRequest(Guid id, DateTime startDate, DateTime endDate)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}