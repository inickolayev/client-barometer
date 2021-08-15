using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Models;
using Requests = ClientBarometer.Contracts.Requests;
using Responses = ClientBarometer.Contracts.Responses;

namespace ClientBarometer.Domain.Services
{
    public interface IChatService
    {
        Task<Responses.Message[]> GetMessages(Guid chatId, int takeLast = ChatConsts.MESSAGES_TAKE_DEFAULT, CancellationToken cancellationToken = default);
        Task<Responses.User> GetUser(Guid chatId, CancellationToken cancellationToken);
        Task<Responses.Chat[]> GetChats(CancellationToken cancellationToken);
        Task<Responses.Message> CreateMessage(CreateMessageRequest request, CancellationToken cancellationToken);
        Task<Responses.Chat> CreateChat(CreateChatRequest request, CancellationToken cancellationToken);
        Task<Responses.User> CreateUser(CreateUserRequest request, CancellationToken cancellationToken);
    }
}