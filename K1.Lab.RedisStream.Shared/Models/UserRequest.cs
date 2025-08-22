using System;

namespace K1.Lab.RedisStream.Shared.Models
{
    public sealed class UserRequest
    {
        private UserRequest()
        {
            Identifier = Guid.NewGuid();
            RequestState = RequestState.InProgress;
        }

        public static UserRequest CreateNewRequest(string userName, string requestText) => new UserRequest
        {
            UserName = userName,
            RequestText = requestText
        };

        public Guid Identifier { get; private set; }
        public string UserName { get; set; }
        public string RequestText { get; set; }
        public RequestState RequestState { get; set; }
    }
}