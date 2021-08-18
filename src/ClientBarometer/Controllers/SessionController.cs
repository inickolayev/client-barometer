using System;
using System.Collections;
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
    public class SessionController : BaseApiController
    {
        private readonly IChatService _chatService;
        private readonly ISourceProcessor _sourceProcessor;
        private readonly IBarometerService _barometerService;
        private readonly ISuggestionService _suggestionService;
        private readonly ILogger<SessionController> _logger;
        
        public SessionController(IChatService chatService,
            ISourceProcessor sourceProcessor,
            IBarometerService barometerService,
            ISuggestionService suggestionService,
            ILogger<SessionController> logger)
        {
            _chatService = chatService;
            _sourceProcessor = sourceProcessor;
            _barometerService = barometerService;
            _suggestionService = suggestionService;
            _logger = logger;
        }

        [HttpGet("messages")]
        [ProducesResponseType(typeof(IEnumerable<Message>), 200)]
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
        [ProducesResponseType(typeof(IEnumerable<Chat>), 200)]
        public async Task<IActionResult> GetChats(CancellationToken cancellationToken)
        {
            var chats = await _chatService.GetChats(cancellationToken);
            return Ok(chats);
        }
                
        [HttpGet("user")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> GetUser(Guid chatId, CancellationToken cancellationToken)
        {
            var user = await _chatService.GetUser(chatId, cancellationToken);
            return Ok(user);
        }

        [HttpGet("barometer")]
        [ProducesResponseType(typeof(BarometerValue), 200)]
        public async Task<BarometerValue> GetBarometerValue(Guid chatId, CancellationToken cancellationToken)
            => await _barometerService.GetValue(chatId, cancellationToken);


        [HttpGet("suggestions")]
        [ProducesResponseType(typeof(Suggestions), 200)]
        public async Task<Suggestions> GetSuggestions(Guid chatId, CancellationToken cancellationToken)
            => await _suggestionService.GetSuggestions(chatId, cancellationToken);
    }
}
