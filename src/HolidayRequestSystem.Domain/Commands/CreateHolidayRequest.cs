using System;
using MediatR;

namespace HolidayRequestSystem.Domain.Commands
{
    public class CreateHolidayRequest : IRequest
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}