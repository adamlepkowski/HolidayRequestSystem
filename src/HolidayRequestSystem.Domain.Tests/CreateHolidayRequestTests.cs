using System;
using System.Collections.Generic;
using HolidayRequestSystem.Domain.CommandHandlers;
using HolidayRequestSystem.Domain.Commands;
using HolidayRequestSystem.Domain.Domain.Exceptions;
using HolidayRequestSystem.Domain.Events;
using HolidayRequestSystem.Domain.Tests.Helpers;
using HolidayRequestSystem.Domain.Utils;
using NUnit.Framework;

namespace HolidayRequestSystem.Domain.Tests
{
    [TestFixture]
    public class CreateHolidayRequestTests : BaseTest
    {
        [Test]
        public void When_create_holiday_request_then_holiday_request_created()
        {
            GuidGenerator.GenerateGuid = () => Guid.Empty; // todo: probabbly will go to base class when more tests created

            var startDate = new DateTime(2017, 9, 1);
            var endDate = new DateTime(2017, 9, 5);

            When(() => new CreateHolidayRequestHandler(this.TestEventStoreRepository).Handle(
                new CreateHolidayRequest
                {
                    UserId = Guid.Empty,
                    StartDate = startDate,
                    EndDate = endDate
                }));
            Then(new HolidayRequestCreated(GuidGenerator.NewGuid(), startDate, endDate));
        }

        [TestCaseSource(nameof(HolidayDateRanges))]
        public void When_create_holiday_for_date_that_already_exists_then_validation_exception_thrown(DateTime startDate, DateTime endDate)
        {
            Given(new HolidayRequestCreated(GuidGenerator.NewGuid(), new DateTime(2017, 9, 8), new DateTime(2017, 9, 28)));
            When(() => new CreateHolidayRequestHandler(this.TestEventStoreRepository)
            .Handle(new CreateHolidayRequest
            {
                UserId = Guid.Empty,
                StartDate = startDate,
                EndDate = endDate
            }));
            ThenExceptionThrown<HolidayRequestAlreadyExistsForSpecifiedDateRange>();
        }

        private static IEnumerable<TestCaseData> HolidayDateRanges()
        {
            yield return new TestCaseData(new DateTime(2017, 9, 5), new DateTime(2017, 9, 9));
            yield return new TestCaseData(new DateTime(2017, 9, 10), new DateTime(2017, 9, 15));
            yield return new TestCaseData(new DateTime(2017, 9, 25), new DateTime(2017, 9, 29));
        }
    }
}