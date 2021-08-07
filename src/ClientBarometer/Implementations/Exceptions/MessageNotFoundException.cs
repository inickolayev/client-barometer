using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Exceptions
{
    public class MessageNotFoundException : Exception
    {
        public Guid MessageId { get; }

        public MessageNotFoundException(Guid messageId, string message)
            : base(message)
        {
            MessageId = messageId;
        }
    }
}
