using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientBarometer.Implementations.Repositories
{
    public class ChatReadRepository : IChatReadRepository
    {
        private readonly IQueryable<Chat> _chats;

        public ChatReadRepository(IQueryable<Chat> chats)
        {
            _chats = chats.AsNoTracking();
        }
        
        public async Task<Chat> Get(Guid chatId, CancellationToken cancellationToken)
            => await _chats
            .FirstOrDefaultAsync(ch => ch.Id == chatId, cancellationToken);
        
        public async Task<bool> Contains(Guid chatId, CancellationToken cancellationToken)
            => await _chats
                .AnyAsync(ch => ch.Id == chatId, cancellationToken);
        
        public async Task<bool> ContainsBySource(Guid sourceId, CancellationToken cancellationToken)
            => await _chats
                .AnyAsync(ch => ch.SourceId == sourceId, cancellationToken);

        public async Task<Chat[]> GetChats(Guid chatId, int skip, int take, CancellationToken cancellationToken)
            => await _chats
            .Where(ch => ch.Id == chatId)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(cancellationToken);
    }
}
