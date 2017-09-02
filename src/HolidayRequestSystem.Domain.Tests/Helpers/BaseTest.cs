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
        private Type _expectedExceptionType;
        private Exception _actionException;

        [SetUp]
        public void Init()
        {
            this.TestEventStoreRepository = new TestEventStoreRepository();
            this._expectedEvents = new List<IEvent>();
        }

        [TearDown]
        public void Cleanup()
        {
            if (_expectedExceptionType != null)
            {
                Assert.IsNotNull(this._expectedExceptionType); // exception occured but not specyfied in ThenExceptionThrown<>()
                Assert.IsInstanceOf(this._expectedExceptionType, this._actionException);
            }
            else
            {
                this.AreEqual(this._expectedEvents, this.TestEventStoreRepository.PublishedEvents);
            }
        }

        protected void Given(IEvent @event)
        {
            this.TestEventStoreRepository.AddGivenEvent(@event);
        }

        protected void When(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                this._actionException = e;
            }

        }

        protected void Then(IEvent @event)
        {
            this._expectedEvents.Add(@event);
        }

        protected void ThenExceptionThrown<T>()
        {
            this._expectedExceptionType = typeof(T);
        }

        private void AreEqual(object expected, object actual)
        {
            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}