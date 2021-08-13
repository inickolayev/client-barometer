using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;

namespace ClientBarometer.Implementations.Exceptions
{
    public class ChatNotFoundException : Exception
    {
        public string ChatId { get; }

        public ChatNotFoundException(string chatId, string message = "")
            : base(message)
        {
            ChatId = chatId;
        }
    }
}
