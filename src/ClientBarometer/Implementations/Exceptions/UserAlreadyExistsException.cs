using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public string SourceId { get; }

        public UserAlreadyExistsException(string sourceId, string message = "")
            : base(message)
        {
            SourceId = sourceId;
        }
    }
}
