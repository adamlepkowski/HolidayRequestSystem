using System;
using HolidayRequestSystem.Domain.Common.Events;
using HolidayRequestSystem.Domain.Tests.Helpers;
using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.CommandHandlers;
using HolidayRequestSystem.Domain.Write.Commands;
using NUnit.Framework;

namespace HolidayRequestSystem.Domain.Tests.Write.CommandHandlers
{
    [TestFixture]
    public class CreateUserHandlerTests : BaseComandTest<CreateUserHandler, CreateUser>
    {
        [Test]
        public void When_create_user_then_user_created()
        {
            GuidGenerator.GenerateGuid = () => Guid.Empty;
            string login = "admin";
            string password = "pass";

            When(new CreateUser
            {
                Login = login,
                Md5Password = password
            });
            Then(new UserCreated(GuidGenerator.NewGuid(), login, password));
        }
    }
}