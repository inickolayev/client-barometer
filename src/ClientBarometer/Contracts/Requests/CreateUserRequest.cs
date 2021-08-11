using System;

namespace ClientBarometer.Contracts.Requests
{
    public class CreateUserRequest
    {
        public string SourceId { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}