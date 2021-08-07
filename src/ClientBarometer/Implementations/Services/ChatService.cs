using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using ClientBarometer.Domain.Services;
using ClientBarometer.Domain.UnitsOfWork;
using ClientBarometer.Implementations.Exceptions;
using ClientBarometer.Implementations.Mappers;
using Microsoft.Extensions.Caching.Memory;
using Requests = ClientBarometer.Contracts.Requests;
using Responses = ClientBarometer.Contracts.Responses;

namespace ClientBarometer.Implementations.Services
{
    public class ChatService : IChatService
    {
        private readonly IMessageReadRepository _messageReadRepository;
        private readonly IChatReadRepository _chatReadRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IChatUnitOfWork _chatUnitOfWork;

        private static IMapper ChatMapper => Create.ChatMapper.Please;

        public ChatService(
            IMessageReadRepository messageReadRepository,
            IChatReadRepository chatReadRepository,
            IUserReadRepository userReadRepository,
            IChatUnitOfWork chatUnitOfWork,
            IMemoryCache memoryCache)
        {
            _messageReadRepository = messageReadRepository;
            _chatReadRepository = chatReadRepository;
            _userReadRepository = userReadRepository;
            _chatUnitOfWork = chatUnitOfWork;
        }

        public async Task<Responses.Message[]> GetMessages(Guid chatId, int takeLast,
            CancellationToken cancellationToken)
        {
            var messages = 
                await _messageReadRepository.GetLastMessages(chatId, takeLast, cancellationToken);
            var users = await _userReadRepository.GetUsers(chatId, 0, int.MaxValue, cancellationToken);
            var result = ChatMapper.Map<Responses.Message[]>(messages);
            foreach (var mess in result)
                mess.Username = users.FirstOrDefault(us => us.Id == mess.Id)?.Name ?? "Unknown user";
            return result;
        }

        public async Task<Responses.User[]> GetUsers(Guid chatId, CancellationToken cancellationToken)
            => ChatMapper.Map<Responses.User[]>(await _userReadRepository.GetUsers(chatId, 0, int.MaxValue, cancellationToken));

        public async Task<Responses.Message> CreateMessage(Requests.CreateMessageRequest request, CancellationToken cancellationToken)
        {
            var newMessage = ChatMapper.Map<Message>(request);

            if (await _chatReadRepository.Contains(request.ChatId, cancellationToken))
            {
                throw new ChatNotFoundException(request.ChatId);
            }
            if (await _userReadRepository.Contains(request.UserId, cancellationToken))
            {
                throw new UserNotFoundException(request.UserId);
            }
            
            _chatUnitOfWork.Messages.RegisterNew(newMessage);

            await _chatUnitOfWork.Complete(cancellationToken);

            return ChatMapper.Map<Responses.Message>(newMessage);
        }

        public async Task<Responses.Chat> CreateChat(Requests.CreateChatRequest request, CancellationToken cancellationToken)
        {
            var newChat = ChatMapper.Map<Chat>(request);

            if (await _chatReadRepository.ContainsBySource(request.SourceId, cancellationToken))
            {
                throw new ChatAlreadyExistsException(request.SourceId);
            }
            
            _chatUnitOfWork.Chats.RegisterNew(newChat);

            await _chatUnitOfWork.Complete(cancellationToken);

            return ChatMapper.Map<Responses.Chat>(newChat);
        }

        public async Task<Responses.User> CreateUser(Requests.CreateUserRequest request, CancellationToken cancellationToken)
        {
            var newUser = ChatMapper.Map<User>(request);

            if (await _chatReadRepository.ContainsBySource(request.SourceId, cancellationToken))
            {
                throw new UserAlreadyExistsException(request.SourceId);
            }
            
            _chatUnitOfWork.Users.RegisterNew(newUser);

            await _chatUnitOfWork.Complete(cancellationToken);

            return ChatMapper.Map<Responses.User>(newUser);
        }
    }
}