using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Exceptions
{
    public class ChatAlreadyExistsException : Exception
    {
        public Guid SourceId { get; }

        public ChatAlreadyExistsException(Guid sourceId, string message = "")
            : base(message)
        {
            SourceId = sourceId;
        }
    }
}
