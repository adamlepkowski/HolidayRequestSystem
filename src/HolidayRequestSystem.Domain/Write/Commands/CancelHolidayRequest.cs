using System;
using MediatR;

namespace HolidayRequestSystem.Domain.Write.Commands
{
    public class CancelHolidayRequest : IRequest
    {
        public Guid UserId { get; set; }
        public Guid HolidayRequestId { get; set; }
    }
}