using System;
using System.ComponentModel.DataAnnotations;

namespace ClientBarometer.Contracts.Responses
{
    public class Chat
    {
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public string Source { get; set; }
    }
}