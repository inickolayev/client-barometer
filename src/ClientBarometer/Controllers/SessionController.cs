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
        private readonly ILogger<SessionController> _logger;
        
        public SessionController(IChatService chatService, ILogger<SessionController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [HttpGet("messages")]
        public async Task<IEnumerable<Message>> GetMessages(CancellationToken cancellationToken)
            => await _chatService.GetMessages(ChatConsts.DEFAULT_CHAT_ID, ChatConsts.MESSAGES_TAKE_DEFAULT, cancellationToken);
                
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody]CreateMessageRequest request, CancellationToken cancellationToken)
        {
            await _chatService.CreateMessage(request, cancellationToken);
            return Ok();
        }

        [HttpGet("barometer")]
        public async Task<int> GetBarometerValue(CancellationToken cancellationToken)
        {
            var messages = await _chatService.GetMessages(ChatConsts.DEFAULT_CHAT_ID, ChatConsts.MESSAGES_TAKE_DEFAULT, cancellationToken);
            var result = messages.Sum(m =>
                    m.Text.Count(ch => int.TryParse(ch.ToString(), out var intVal) && intVal % 2 == 0) * 10
                ) % 1000;
            return result;
        }
    }
}
