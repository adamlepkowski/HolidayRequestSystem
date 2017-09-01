using System;
using System.Linq;
using HolidayRequestSystem.Domain.CommandHandlers;
using HolidayRequestSystem.Domain.Commands;
using HolidayRequestSystem.Domain.Events;
using HolidayRequestSystem.Domain.Tests.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HolidayRequestSystem.Domain.Tests
{
    [TestFixture]
    public class CreateHolidayRequestTests
    {
        [Test]
        public void when_create_holiday_request_then_holiday_request_created()
        {
            var createHolidayRequest = new CreateHolidayRequest() { UserId = Guid.Empty };

            var testEventStoreRepositoryMock = new TestEventStoreRepository();

            new CreateHolidayRequestHandler(testEventStoreRepositoryMock).Handle(createHolidayRequest);


            var publishedEvents = testEventStoreRepositoryMock.PublishedEvents;

            var expectedResult = new HolidayRequestCreated(Guid.Empty, new DateTime(), new DateTime());

            AreEqual(expectedResult, publishedEvents.First());

        }

        private static void AreEqual(object expected, object actual)
        {
            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}