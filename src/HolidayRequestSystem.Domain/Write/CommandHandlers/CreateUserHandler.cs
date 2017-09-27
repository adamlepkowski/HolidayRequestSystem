using HolidayRequestSystem.Domain.Utils;
using HolidayRequestSystem.Domain.Write.Commands;
using HolidayRequestSystem.Domain.Write.Model;
using MediatR;

namespace HolidayRequestSystem.Domain.Write.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser>
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public CreateUserHandler(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Handle(CreateUser message)
        {
            var user = new User(message.Login, message.Md5Password);

            _eventStoreRepository.Save(user);
        }
    }
}