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
    public class CancelHolidayRequestHandlerTests : BaseComandTest<CancelHolidayRequestHandler, CancelHolidayRequest>
    {
        private readonly Guid _holidayRequestId = new Guid("32A388F5-0378-4112-BD5E-28D9F73D50C4");

        [Test]
        public void When_cancel_not_existing_holiday_request_then_validation_exception_thrown()
        {
            Guid existingHolidayRequestId = this._holidayRequestId;
            Guid notExistingHolidayRequestId = Guid.NewGuid();

            Given(new HolidayRequestCreated(existingHolidayRequestId, new DateTime(2017, 9, 8),
                new DateTime(2017, 10, 10), Guid.NewGuid(), Guid.NewGuid(), DateTimeProvider.Now));
            When(new CancelHolidayRequest
            {
                UserId = Guid.Empty,
                HolidayRequestId = notExistingHolidayRequestId
            });
            ThenExceptionThrown<EntityNotExists>(); // TODO: improve it to allow verify attributes of the exception as an boolean expression
        }

        [Test]
        public void When_cancel_holiday_request_then_holiday_request_canceled()
        {
            DateTimeProvider.GenerateDateTimeNow = () => new DateTime(2017, 10, 10, 12, 30, 30);

            Given(new HolidayRequestCreated(_holidayRequestId, new DateTime(2017, 9, 8),
                new DateTime(2017, 10, 10), Guid.NewGuid(), Guid.NewGuid(), DateTimeProvider.Now));
            When(new CancelHolidayRequest
            {
                UserId = Guid.Empty,
                HolidayRequestId = _holidayRequestId
            });
            Then(new HolidayRequestCanceled(_holidayRequestId, DateTimeProvider.Now));
        }
    }
}