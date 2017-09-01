using System;
using HolidayRequestSystem.Domain.CommandHandlers;
using HolidayRequestSystem.Domain.Commands;
using HolidayRequestSystem.Domain.Events;
using HolidayRequestSystem.Domain.Tests.Helpers;
using NUnit.Framework;

namespace HolidayRequestSystem.Domain.Tests
{
    [TestFixture]
    public class CreateHolidayRequestTests : BaseTest
    {
        [Test]
        public void when_create_holiday_request_then_holiday_request_created()
        {
            var createHolidayRequest = new CreateHolidayRequest() { UserId = Guid.Empty };

            When(() => new CreateHolidayRequestHandler(this.TestEventStoreRepository).Handle(createHolidayRequest));
            Then(new HolidayRequestCreated(Guid.Empty, new DateTime(), new DateTime()));
        }
    }
}