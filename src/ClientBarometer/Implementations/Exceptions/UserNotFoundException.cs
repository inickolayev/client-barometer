using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string UserId { get; }

        public UserNotFoundException(string userId, string message = "")
            : base(message)
        {
            UserId = userId;
        }
    }
}
