using MediatR;

namespace HolidayRequestSystem.Domain.Write.Commands
{
    public class CreateUser : IRequest
    {
        public string Login { get; set; }
        public string Md5Password { get; set; }
    }
}