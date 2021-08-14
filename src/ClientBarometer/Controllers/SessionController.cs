using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Contracts.Responses;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientBarometer.Controllers
{
    [ApiController]
    [Route("session")]
    public class SessionController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly ISourceProcessor _sourceProcessor;
        private readonly ILogger<SessionController> _logger;
        
        public SessionController(IChatService chatService, ISourceProcessor sourceProcessor, ILogger<SessionController> logger)
        {
            _chatService = chatService;
            _sourceProcessor = sourceProcessor;
            _logger = logger;
        }

        [HttpGet("messages")]
        public async Task<IEnumerable<Message>> GetMessages(Guid chatId, CancellationToken cancellationToken)
            => await _chatService.GetMessages(chatId, ChatConsts.MESSAGES_TAKE_DEFAULT, cancellationToken);
                
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody]CreateMessageRequest request, CancellationToken cancellationToken)
        {
            request.UserSourceId = ChatConsts.DEFAULT_USER_SOURCE_ID;
            // TODO: Remove source attribute in "create to source"
            request.Source = ChatConsts.TELEGRAM_SOURCE;
            await _sourceProcessor.ProcessToSource(request, cancellationToken);
            return Ok();
        }
        
        [HttpGet("chats")]
        public async Task<IActionResult> GetChats(CancellationToken cancellationToken)
        {
            var chats = await _chatService.GetChats(cancellationToken);
            return Ok(chats);
        }
                
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(Guid chatId, CancellationToken cancellationToken)
        {
            var users = await _chatService.GetUsers(chatId, cancellationToken);
            return Ok(users);
        }

        [HttpGet("barometer")]
        public async Task<int> GetBarometerValue(Guid chatId, CancellationToken cancellationToken)
        {
            var messages = await _chatService.GetMessages(chatId, ChatConsts.MESSAGES_TAKE_DEFAULT, cancellationToken);
            var result = messages.Sum(m =>
                    m?.Text?.Count(ch => int.TryParse(ch.ToString(), out var intVal) && intVal % 2 == 0) * 10 ?? 0
                ) % 1000;
            return result;
        }
    }
}
