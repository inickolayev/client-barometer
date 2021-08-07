using System;

namespace ClientBarometer.Contracts.Requests
{
    public class CreateUserRequest
    {
        public Guid SourceId { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}