using System;
using System.ComponentModel.DataAnnotations;

namespace ClientBarometer.Contracts.Responses
{
    public class User
    {
        public Guid Id { get; set; }
        public string SourceId { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}