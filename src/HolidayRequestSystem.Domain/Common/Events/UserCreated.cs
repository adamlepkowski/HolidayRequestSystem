using System;
using HolidayRequestSystem.Domain.Utils;

namespace HolidayRequestSystem.Domain.Common.Events
{
    [EventTypeId("776ec9ff-4daa-4812-8e13-549c1c8dc0b1")]
    public class UserCreated : IEvent
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Md5Password { get; set; }

        public UserCreated(Guid Id, string login, string md5Password)
        {
            this.Id = Id;
            this.Login = login;
            this.Md5Password = md5Password;
        }
    }
}