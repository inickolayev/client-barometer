using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.DataAccess;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientBarometer.Implementations.Repositories
{
    public class MessageReadRepository : IMessageReadRepository
    {
        private readonly IQueryable<Message> _messages;

        public MessageReadRepository(ClientBarometerDbContext dbContext)
        {
            _messages = dbContext.Messages.AsNoTracking();
        }

        public async Task<Message> Get(Guid messageId, CancellationToken cancellationToken)
            => await _messages
            .FirstOrDefaultAsync(ms => ms.Id.Equals(messageId), cancellationToken);

        public async Task<Message[]> GetMessages(Guid chatId, int skip, int take, CancellationToken cancellationToken)
            => await _messages
            .Where(ms => ms.ChatId == chatId)
            .OrderByDescending(mes => mes.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(cancellationToken);
        
        public async Task<Message[]> GetLastMessages(Guid chatId, int takeLast, CancellationToken cancellationToken)
            => await _messages
                .Where(ms => ms.ChatId == chatId)
                .OrderByDescending(ms => ms.CreatedAt)
                // .TakeLast(takeLast)
                .ToArrayAsync(cancellationToken);
    }
}
