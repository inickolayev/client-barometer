using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using ClientBarometer.Domain.Services;
using ClientBarometer.Implementations.Exceptions;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace ClientBarometer.Implementations.Services
{
    public class SourceProcessor : ISourceProcessor
    {
        private readonly IChatService _chatService;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IChatReadRepository _chatReadRepository;
        private readonly TelegramBotClient _tgClient;
        private readonly ILogger<SourceProcessor> _logger;

        public SourceProcessor(IChatService chatService,
            IUserReadRepository userReadRepository,
            IChatReadRepository chatReadRepository,
            TelegramBotClient tgClient,
            ILogger<SourceProcessor> logger)
        {
            _chatService = chatService;
            _userReadRepository = userReadRepository;
            _chatReadRepository = chatReadRepository;
            _tgClient = tgClient;
            _logger = logger;
        }
        
        public async Task ProcessToSource(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            switch(request.Source)
            {
                case ChatConsts.TELEGRAM_SOURCE:
                    await ProcessToTelegram(request, cancellationToken);
                    break;
                default:
                    throw new SourceNotFoundException(request.Source);
            };
        }
        
        public async Task ProcessFromSource(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            switch(request.Source)
            {
                case ChatConsts.TELEGRAM_SOURCE:
                    await ProcessFromTelegram(request, cancellationToken);
                    break;
                default:
                    throw new SourceNotFoundException(request.Source);
            };
        }

        private async Task ProcessToTelegram(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            await _chatService.CreateMessage(request, cancellationToken);
            var chat = await _chatReadRepository.Get(request.ChatId, cancellationToken);
            await _tgClient.SendTextMessageAsync(chat.SourceId, request.Text,
                    cancellationToken: cancellationToken);
        }

        private async Task ProcessFromTelegram(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            if (!await _chatReadRepository.Contains(request.ChatSourceId, cancellationToken))
            {
                await CreateChatOnInit(request, cancellationToken);
            }
            if (!await _userReadRepository.Contains(request.UserSourceId, cancellationToken))
            {
                await CreateUserOnInit(request, cancellationToken);
            }
            await _chatService.CreateMessage(request, cancellationToken);
        }

        private async Task CreateChatOnInit(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            var createChat = new CreateChatRequest
            {
                SourceId = request.ChatSourceId,
                Source = ChatConsts.TELEGRAM_SOURCE
            };
            await _chatService.CreateChat(createChat, cancellationToken);
        }
        
        private async Task CreateUserOnInit(CreateMessageRequest request, CancellationToken cancellationToken)
        {
            // TODO: get name and birthday from source
            var createUser = new CreateUserRequest
            {
                SourceId = request.UserSourceId,
                Source = ChatConsts.TELEGRAM_SOURCE,
                Birthday = DateTime.Now,
                Name = request.UserSourceId
            };
            await _chatService.CreateUser(createUser, cancellationToken);
        }
    }
}