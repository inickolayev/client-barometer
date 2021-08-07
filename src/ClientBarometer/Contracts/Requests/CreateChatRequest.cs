using System;

namespace ClientBarometer.Contracts.Requests
{
    public class CreateChatRequest
    {
        public string SourceId { get; set; }
        public string Source { get; set; }
    }
}