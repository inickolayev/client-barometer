using System;

namespace ClientBarometer.Contracts.Requests
{
    public class CreateChatRequest
    {
        public Guid SourceId { get; set; }
        public string Source { get; set; }
    }
}