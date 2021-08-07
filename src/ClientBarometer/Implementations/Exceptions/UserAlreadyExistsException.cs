using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public Guid SourceId { get; }

        public UserAlreadyExistsException(Guid sourceId, string message = "")
            : base(message)
        {
            SourceId = sourceId;
        }
    }
}
