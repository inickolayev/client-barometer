using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientBarometer.Controllers
{
    [ApiController]
    [Route("session")]
    public class SessionController : ControllerBase
    {
        private static readonly ConcurrentStack<ChatMessage> Messages = new ConcurrentStack<ChatMessage>(new[]
        {
            new ChatMessage {
                Id = "0",
                RoomId = "123",
                Text = "Hello",
                Username = "another",
                CreatedAt = DateTime.UtcNow
            },
            new ChatMessage {
                Id = "1",
                RoomId = "123",
                Text = "World",
                Username = "some",
                CreatedAt = DateTime.UtcNow
            }
        });

        private static readonly Timer Timer = new Timer(obj =>
        {
            if (Messages.Count() > 100)
            {
                Messages.Clear();
            }
            Messages.Push(new ChatMessage
            {
                Id = Messages.Count.ToString(),
                RoomId = "123",
                Text = new Random().Next().ToString(),
                Username = new Random().Next() % 2 == 0 ? "another" : "some",
                CreatedAt = DateTime.UtcNow
            });
        }, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(5));

        private readonly ILogger<SessionController> _logger;
        
        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("messages")]
        public IEnumerable<ChatMessage> GetMessages()
            => Messages;
                
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody]SendMessageRequest request)
        {
            var newMessage = new ChatMessage
            {
                Id = Messages.Count().ToString(),
                RoomId = "123",
                CreatedAt = DateTime.UtcNow,
                Text = request.Text,
                Username = "some"
            };
            Messages.Push(newMessage);
            return Ok();
        }
        
        [HttpGet("barometer")]
        public int GetBarometerValue()
            => (Messages.Sum(m =>
                    m.Text.Count(ch =>
                        int.TryParse(ch.ToString(), out var intVal) && intVal % 2 == 0)) * 10
            ) % 1000;
    }
}
