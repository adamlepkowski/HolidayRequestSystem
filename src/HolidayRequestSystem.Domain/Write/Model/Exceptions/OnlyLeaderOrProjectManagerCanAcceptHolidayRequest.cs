using System;

namespace HolidayRequestSystem.Domain.Write.Model.Exceptions
{
    public class OnlyLeaderOrProjectManagerCanAcceptHolidayRequest : Exception
    {
        public readonly Guid AccepterId;

        public OnlyLeaderOrProjectManagerCanAcceptHolidayRequest(Guid accepterId)
        {
            this.AccepterId = accepterId;
        }
    }
}