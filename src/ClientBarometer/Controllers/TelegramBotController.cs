using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ClientBarometer.Controllers
{
    public class TelegramBotController : BaseApiController
    {
        private readonly ILogger<TelegramBotController> _logger;
        private readonly TelegramBotClient _client;
        private readonly IChatService _chatService;
        private readonly ISourceProcessor _sourceProcessor;

        public TelegramBotController(ILogger<TelegramBotController> logger,
            TelegramBotClient client,
            IChatService chatService,
            ISourceProcessor sourceProcessor)
        {
            _logger = logger;
            _client = client;
            _chatService = chatService;
            _sourceProcessor = sourceProcessor;
        }

        [HttpGet]
        public async Task<User> GetAsync()
        {
            return await _client.GetMeAsync();
        }
        
        [HttpPost]
        public async Task HandleAsync(Update update, CancellationToken token)
        {
            if (update == null)
                throw new Exception("Update is null");
            if (update.Type == UpdateType.Message)
                await InternalHandleMessageAsync(update, token);
            else if (update.Type == UpdateType.EditedMessage)
                await HandleEditedMessageAsync(update, token);
        }

        private async Task InternalHandleMessageAsync(Update update, CancellationToken token)
        {
            var newMessage = new CreateMessageRequest
            {
                ChatSourceId = update.Message.Chat.Id.ToString(),
                UserSourceId = update.Message.Chat.Username,
                Source = ChatConsts.TELEGRAM_SOURCE,
                Text = update.Message.Text
            };
            await _sourceProcessor.ProcessFromSource(newMessage, token);
        }

        private async Task HandleEditedMessageAsync(Update update, CancellationToken token)
        {
            // await _client.SendTextMessageAsync(update.EditedMessage.Chat.Id, "Опа, да тут сообщения правят -_-", cancellationToken: token);
        }
    }
}
