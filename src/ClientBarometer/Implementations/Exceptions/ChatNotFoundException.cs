using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Exceptions
{
    public class ChatNotFoundException : Exception
    {
        public Guid ChatId { get; }

        public ChatNotFoundException(Guid chatId, string message = "")
            : base(message)
        {
            ChatId = chatId;
        }
    }
}
