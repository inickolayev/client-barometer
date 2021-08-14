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
    public class ChatReadRepository : IChatReadRepository
    {
        private readonly ClientBarometerDbContext _dbContext;
        private readonly IQueryable<Chat> _chats;

        public ChatReadRepository(ClientBarometerDbContext dbContext)
        {
            _dbContext = dbContext;
            _chats = dbContext.Chats.AsNoTracking();
        }
        
        public async Task<Chat> Get(Guid chatId, CancellationToken cancellationToken)
            => await _chats
            .FirstOrDefaultAsync(ch => ch.Id == chatId, cancellationToken);
        
        public async Task<Chat> Get(string sourceId, CancellationToken cancellationToken)
            => await _chats
                .FirstOrDefaultAsync(ch => ch.SourceId == sourceId, cancellationToken);

        public async Task<bool> Contains(Guid chatId, CancellationToken cancellationToken)
            => await _chats
                .AnyAsync(ch => ch.Id == chatId, cancellationToken);
        
        public async Task<bool> Contains(string sourceId, CancellationToken cancellationToken)
            => await _chats
                .AnyAsync(ch => ch.SourceId == sourceId, cancellationToken);

        public async Task<Chat[]> GetChats(int skip, int take, CancellationToken cancellationToken)
            => await _chats
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(cancellationToken);
    }
}
