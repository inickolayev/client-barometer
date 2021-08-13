using System;
using AutoMapper;
using ClientBarometer.Common.Abstractions.Mappers;
using Models = ClientBarometer.Domain.Models;
using Requests = ClientBarometer.Contracts.Requests;
using Responses = ClientBarometer.Contracts.Responses;

namespace ClientBarometer.Implementations.Mappers
{
    public class ChatMapperBuilder : IMapperDsl
    {
        public static MapperConfiguration MapperConfiguration => new MapperConfiguration(c =>
        {
            c.CreateMap<Requests.CreateChatRequest, Models.Chat>();
            c.CreateMap<Requests.CreateUserRequest, Models.User>();
            c.CreateMap<Requests.CreateMessageRequest, Models.Message>();
            c.CreateMap<Models.Message, Responses.Message>();
            c.CreateMap<Models.Chat, Responses.Chat>();
            c.CreateMap<Models.User, Responses.User>();
        });

        public IMapper Please => MapperConfiguration.CreateMapper();
    }
}
