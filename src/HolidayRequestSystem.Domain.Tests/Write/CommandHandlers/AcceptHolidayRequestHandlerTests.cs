using System;
using HolidayRequestSystem.Domain.Common.Events;
using HolidayRequestSystem.Domain.Tests.Helpers;
using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.CommandHandlers;
using HolidayRequestSystem.Domain.Write.Commands;
using HolidayRequestSystem.Domain.Write.Model.Exceptions;
using NUnit.Framework;

namespace HolidayRequestSystem.Domain.Tests.Write.CommandHandlers
{
    [TestFixture]
    public class AcceptHolidayRequestHandlerTests : BaseTest
    {
        private readonly Guid _leaderId = new Guid("40A388F5-0378-4112-BD5E-28D9F73D50C4");
        private readonly Guid _projectManagerId = new Guid("DF5CFF5A-8152-4869-A0BD-EDECE726CFC3");
        private readonly Guid _holidayRequestId = new Guid("32A388F5-0378-4112-BD5E-28D9F73D50C4");

        [Test]
        public void When_accept_not_existing_holiday_request_then_validation_exception_thrown()
        {
            Guid existingHolidayRequestId = this._holidayRequestId;
            Guid notExistingHolidayRequestId = Guid.NewGuid();

            Given(new HolidayRequestCreated(existingHolidayRequestId, new DateTime(2017, 9, 8),
                new DateTime(2017, 10, 10), Guid.NewGuid(), Guid.NewGuid(), DateTimeProvider.Now));
            When(() => new AcceptHolidayRequestHandler(this.TestEventStoreRepository).Handle(
                new AcceptHolidayRequest
                {
                    UserId = Guid.Empty,
                    AccepterId = Guid.Empty,
                    HolidayRequestId = notExistingHolidayRequestId
                }
            ));
            ThenExceptionThrown<EntityNotExists>(); // TODO: improve it to allow verify attributes of the exception as an boolean expression
        }

        [Test]
        public void When_not_allowed_person_try_to_accept_holiday_request_then_validation_exception_thrown()
        {
            Guid notProjectManagerOrLeaderId = new Guid("77a1111c-92b5-42fe-ba69-55bc88eb7a67");

            Given(new HolidayRequestCreated(this._holidayRequestId, new DateTime(2017, 9, 8),
                new DateTime(2017, 10, 10), this._leaderId, this._projectManagerId, DateTimeProvider.Now));
            When(() => new AcceptHolidayRequestHandler(this.TestEventStoreRepository).Handle(
                new AcceptHolidayRequest
                {
                    UserId = Guid.Empty,
                    AccepterId = notProjectManagerOrLeaderId,
                    HolidayRequestId = this._holidayRequestId
                }
            ));
            ThenExceptionThrown<OnlyLeaderOrProjectManagerCanAcceptHolidayRequest>(); // TODO: improve it to allow verify attributes of the exception as an boolean expression
        }

        [Test]
        public void When_leader_accept_holiday_request_then_holiday_request_accepted()
        {
            DateTimeProvider.GenerateDateTimeNow = () => new DateTime(2017, 10, 10, 12, 30, 30);

            Given(new HolidayRequestCreated(this._holidayRequestId, new DateTime(2017, 9, 8),
                new DateTime(2017, 10, 10), this._leaderId, this._projectManagerId, DateTimeProvider.Now));
            Given(new HolidayRequestAcceptedByProjectManager(this._holidayRequestId, this._leaderId, DateTimeProvider.Now));
            When(() => new AcceptHolidayRequestHandler(this.TestEventStoreRepository).Handle(
                new AcceptHolidayRequest
                {
                    UserId = Guid.Empty,
                    AccepterId = this._leaderId,
                    HolidayRequestId = this._holidayRequestId
                }
            ));
            Then(new HolidayRequestAcceptedByLeader(this._holidayRequestId, this._leaderId, DateTimeProvider.Now));
        }

        [Test]
        public void When_project_manager_accept_holiday_request_then_holiday_request_accepted()
        {
            DateTimeProvider.GenerateDateTimeNow = () => new DateTime(2017, 10, 10, 12, 30, 30);

            Given(new HolidayRequestCreated(_holidayRequestId, new DateTime(2017, 9, 8),
                new DateTime(2017, 10, 10), this._leaderId, this._projectManagerId, DateTimeProvider.Now));
            When(() => new AcceptHolidayRequestHandler(this.TestEventStoreRepository).Handle(
                new AcceptHolidayRequest
                {
                    UserId = Guid.Empty,
                    AccepterId = this._projectManagerId,
                    HolidayRequestId = _holidayRequestId
                }
            ));
            Then(new HolidayRequestAcceptedByProjectManager(_holidayRequestId, this._projectManagerId, DateTimeProvider.Now));
        }

        [Test]
        public void When_leader_try_to_accept_before_project_manager_then_validation_exception_thrown()
        {
            DateTimeProvider.GenerateDateTimeNow = () => new DateTime(2017, 10, 10, 12, 30, 30);

            Given(new HolidayRequestCreated(this._holidayRequestId, new DateTime(2017, 9, 8),
                new DateTime(2017, 10, 10), this._leaderId, this._projectManagerId, DateTimeProvider.Now));

            When(() => new AcceptHolidayRequestHandler(this.TestEventStoreRepository).Handle(
                new AcceptHolidayRequest
                {
                    UserId = Guid.Empty,
                    AccepterId = this._leaderId,
                    HolidayRequestId = this._holidayRequestId
                }
            ));
            ThenExceptionThrown<HolidayRequestMustBeFirstAcceptedByProjectManager>();
        }
    }
}