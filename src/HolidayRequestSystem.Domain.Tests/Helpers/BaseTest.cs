using System;
using System.Collections.Generic;
using HolidayRequestSystem.Domain.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HolidayRequestSystem.Domain.Tests.Helpers
{
    public class BaseTest
    {
        protected TestEventStoreRepository TestEventStoreRepository;
        private List<IEvent> _expectedEvents;

        [SetUp]
        public void Init()
        {
            this.TestEventStoreRepository = new TestEventStoreRepository();
            this._expectedEvents = new List<IEvent>();
        }

        [TearDown]
        public void Cleanup()
        {
            this.AreEqual(_expectedEvents, TestEventStoreRepository.PublishedEvents);
        }

        protected void When(Action action)
        {
            action();
        }

        protected void Then(IEvent @event)
        {
            this._expectedEvents.Add(@event);
        }

        private void AreEqual(object expected, object actual)
        {
            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}