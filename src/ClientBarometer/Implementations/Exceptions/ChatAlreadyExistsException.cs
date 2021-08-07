using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Exceptions
{
    public class ChatAlreadyExistsException : Exception
    {
        public string SourceId { get; }

        public ChatAlreadyExistsException(string sourceId, string message = "")
            : base(message)
        {
            SourceId = sourceId;
        }
    }
}
