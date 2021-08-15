using System;
using System.ComponentModel.DataAnnotations;

namespace ClientBarometer.Domain.Models
{
    public class BarometerEntry
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public int Value { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; private set; }
    }
}