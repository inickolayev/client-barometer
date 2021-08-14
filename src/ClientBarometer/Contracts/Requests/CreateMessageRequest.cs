using System;

namespace ClientBarometer.Contracts.Requests
{
    public class CreateMessageRequest
    {
        public string Source { get; set; }
        public string ChatSourceId { get; set; }
        public Guid ChatId { get; set; }
        public string UserSourceId { get; set; }
        public string Text { get; set; }
    }
}