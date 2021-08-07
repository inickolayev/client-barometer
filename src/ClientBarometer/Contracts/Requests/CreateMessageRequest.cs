using System;

namespace ClientBarometer.Contracts.Requests
{
    public class CreateMessageRequest
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}