using System;
using System.Collections.Generic;
using HolidayRequestSystem.Domain.Utils;
using MediatR;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HolidayRequestSystem.Domain.Tests.Helpers
{
    public class BaseComandTest<THandler, TCommand>
        where THandler : IRequestHandler<TCommand>
        where TCommand : IRequest
    {
        protected TestEventStoreRepository TestEventStoreRepository;
        private List<IEvent> _expectedEvents;
        private Type _expectedExceptionType;
        private Exception _actionException;
        private THandler _commandHandler;

        [SetUp]
        public void Init()
        {
            this.TestEventStoreRepository = new TestEventStoreRepository();
            this._expectedEvents = new List<IEvent>();
            this._expectedExceptionType = null;
            this._actionException = null;
            this._commandHandler = InitializeHandler();
        }

        protected virtual THandler InitializeHandler()
        {
            return (THandler)Activator.CreateInstance(typeof(THandler), this.TestEventStoreRepository);
        }

        [TearDown]
        public void Cleanup()
        {
            if (_expectedExceptionType != null)
            {
                Assert.IsNotNull(this._expectedExceptionType); // exception occured but not specyfied in ThenExceptionThrown<>()
                Assert.IsInstanceOf(this._expectedExceptionType, this._actionException);
            }
            else if (_expectedExceptionType == null && _actionException != null)
            {
                throw _actionException;
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

        protected void When(TCommand command)
        {
            try
            {
                this._commandHandler.Handle(command);
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