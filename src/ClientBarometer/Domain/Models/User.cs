using System;
using System.ComponentModel.DataAnnotations;

namespace ClientBarometer.Domain.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string SourceId { get; set; }
        [Required]
        public string Source { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Birthday { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; private set; }

    }
}