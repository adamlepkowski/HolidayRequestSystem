using System;
using System.Collections.Generic;
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
    public class CreateHolidayRequestHandlerTests : BaseTest
    {
        private readonly Guid _leaderId = new Guid("40A388F5-0378-4112-BD5E-28D9F73D50C4");
        private readonly Guid _projectManagerId = new Guid("DF5CFF5A-8152-4869-A0BD-EDECE726CFC3");

        [Test]
        public void When_create_holiday_request_then_holiday_request_created()
        {
            GuidGenerator.GenerateGuid = () => Guid.Empty; // todo: probabbly will go to base class when more tests created
            DateTimeProvider.GenerateDateTimeNow = () => new DateTime(2017, 10, 10, 12, 30, 30);

            var startDate = new DateTime(2017, 9, 1);
            var endDate = new DateTime(2017, 9, 5);

            When(() => new CreateHolidayRequestHandler(this.TestEventStoreRepository).Handle(
                new CreateHolidayRequest
                {
                    UserId = Guid.Empty,
                    StartDate = startDate,
                    EndDate = endDate,
                    LeaderId = this._leaderId,
                    ProjectManagerId = this._projectManagerId
                }));
            Then(new HolidayRequestCreated(GuidGenerator.NewGuid(), startDate, endDate, this._leaderId, this._projectManagerId, DateTimeProvider.Now));
        }

        [TestCaseSource(nameof(HolidayDateRanges))]
        public void When_create_holiday_for_date_that_already_exists_then_validation_exception_thrown(DateTime startDate, DateTime endDate)
        {
            Given(new HolidayRequestCreated(GuidGenerator.NewGuid(), new DateTime(2017, 9, 8),
                new DateTime(2017, 9, 28), this._leaderId, this._projectManagerId, DateTimeProvider.Now));
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