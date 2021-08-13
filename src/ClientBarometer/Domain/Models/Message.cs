using System;
using System.ComponentModel.DataAnnotations;

namespace ClientBarometer.Domain.Models
{
    public class Message
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ChatId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; private set; }
    }
}