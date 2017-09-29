using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Commands;
using HolidayRequestSystem.Domain.Write.Model;
using MediatR;

namespace HolidayRequestSystem.Domain.Write.CommandHandlers
{
    public class CreateUserHandler : CreateAggregateRequestHandler<CreateUser, User>
    {
        public CreateUserHandler(IEventStoreRepository eventStoreRepository) : base(eventStoreRepository)
        {
        }

        protected override void Handle(User aggregate, CreateUser message)
        {
            aggregate.CreateUser(message.Login, message.Md5Password);
        }
    }
}