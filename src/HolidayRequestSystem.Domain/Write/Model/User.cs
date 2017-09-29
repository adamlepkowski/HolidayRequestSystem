using System;
using System.Collections.Generic;
using System.Linq;
using HolidayRequestSystem.Domain.Common.Events;
using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Model.Exceptions;

namespace HolidayRequestSystem.Domain.Write.Model
{
    public class User : AggregateBase
    {
        private string Login { get; set; }
        private string Md5Password { get; set; }
        private IList<HolidayRequest> HolidayRequest { get; set; }

        public User()
        {
            this.HolidayRequest = new List<HolidayRequest>();
        }

        public void CreateUser(string login, string md5Password)
        {
            Publish(new UserCreated(GuidGenerator.NewGuid(), login, md5Password));
        }

        public void CreateHolidayRequest(DateTime startDate, DateTime endDate, Guid leaderId, Guid projectManagerId)
        {
            if (IsInRange(this.HolidayRequest, startDate, endDate))
            {
                throw new HolidayRequestAlreadyExistsForSpecifiedDateRange();
            }

            Publish(new HolidayRequestCreated(GuidGenerator.NewGuid(), startDate, endDate, leaderId, projectManagerId, DateTimeProvider.Now));
        }

        private static bool IsInRange(IList<HolidayRequest> holidayRequests, DateTime startDate, DateTime endDate)
        {
            return holidayRequests.Any(m =>
                m.StartDate < startDate && startDate < m.EndDate
                ||
                m.StartDate < endDate && endDate <= m.EndDate
                ||
                startDate < m.StartDate && m.StartDate < endDate
                ||
                startDate < m.EndDate && m.EndDate <= endDate);
        }

        public void AcceptHolidayRequest(Guid holidayRequestId, Guid accepterId)
        {
            var holidayRequest = this.HolidayRequest.FirstOrDefault(x => x.Id == holidayRequestId);
            if (holidayRequest == null)
            {
                throw new EntityNotExists(typeof(HolidayRequest), holidayRequestId);
            }

            if (holidayRequest.LeaderId != accepterId && holidayRequest.ProjectManagerId != accepterId)
            {
                throw new OnlyLeaderOrProjectManagerCanAcceptHolidayRequest(accepterId);
            }

            if (holidayRequest.LeaderId == accepterId)
            {
                if (!holidayRequest.AcceptedByProjectManager)
                {
                    throw new HolidayRequestMustBeFirstAcceptedByProjectManager();
                }

                Publish(new HolidayRequestAcceptedByLeader(holidayRequestId, accepterId, DateTimeProvider.Now));
            }

            if (holidayRequest.ProjectManagerId == accepterId)
            {
                Publish(new HolidayRequestAcceptedByProjectManager(holidayRequestId, accepterId, DateTimeProvider.Now));
            }
        }

        public void CancelHolidayRequest(Guid holidayRequestId)
        {
            var holidayRequest = this.HolidayRequest.FirstOrDefault(x => x.Id == holidayRequestId);
            if (holidayRequest == null)
            {
                throw new EntityNotExists(typeof(HolidayRequest), holidayRequestId);
            }

            Publish(new HolidayRequestCanceled(holidayRequestId, DateTimeProvider.Now));
        }

        public void Apply(UserCreated @event)
        {
            this.Id = @event.Id;
            this.Login = @event.Login;
            this.Md5Password = @event.Md5Password;
        }

        public void Apply(HolidayRequestCreated @event)
        {
            this.HolidayRequest.Add(new HolidayRequest(@event.Id, @event.StartDate, @event.EndDate, @event.LeaderId, @event.ProjectManagerId));
        }

        public void Apply(HolidayRequestAcceptedByLeader @event)
        {
        }

        public void Apply(HolidayRequestCanceled @event)
        {
        }

        public void Apply(HolidayRequestAcceptedByProjectManager @event)
        {
            this.HolidayRequest.First(x => x.Id == @event.HolidayRequestId).AcceptedByProjectManager = true;
        }
    }
}