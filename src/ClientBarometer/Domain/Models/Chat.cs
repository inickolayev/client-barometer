using System;
using System.ComponentModel.DataAnnotations;

namespace ClientBarometer.Domain.Models
{
    public class Chat
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid SourceId { get; set; }
        [Required]
        public string Source { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; private set; }
    }
}