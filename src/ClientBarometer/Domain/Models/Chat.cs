using System;
using System.ComponentModel.DataAnnotations;

namespace ClientBarometer.Domain.Models
{
    public class Chat
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string SourceId { get; set; }
        [Required]
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; private set; }
    }
}