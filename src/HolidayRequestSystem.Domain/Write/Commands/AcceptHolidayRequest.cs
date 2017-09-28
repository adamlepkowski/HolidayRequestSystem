using System;
using MediatR;

namespace HolidayRequestSystem.Domain.Write.Commands
{
    public class AcceptHolidayRequest : IRequest
    {
        public Guid UserId { get; set; }
        public Guid HolidayRequestId { get; set; }
        public Guid AccepterId { get; set; }
    }
}