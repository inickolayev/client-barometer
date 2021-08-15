using System;

namespace ClientBarometer.Contracts.Responses
{
    public class PersonalInfo
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}