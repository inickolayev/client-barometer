using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ClientBarometer.Implementations.Repositories
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly IQueryable<Chat> _chats;
        private readonly IQueryable<Message> _messages;
        private readonly IQueryable<User> _users;

        public UserReadRepository(IQueryable<User> users, IQueryable<Chat> chats, IQueryable<Message> messages)
        {
            _chats = chats.AsNoTracking();
            _messages = messages.AsNoTracking();
            _users = users.AsNoTracking();
        }
        
        public async Task<User> Get(Guid userId, CancellationToken cancellationToken)
            => await _users
            .FirstOrDefaultAsync(us => us.Id == userId, cancellationToken);

        public async Task<bool> Contains(Guid userId, CancellationToken cancellationToken)
            => await _users
                .AnyAsync(us => us.Id == userId, cancellationToken);
        
        public async Task<bool> ContainsBySource(Guid sourceId, CancellationToken cancellationToken)
            => await _chats
                .AnyAsync(ch => ch.SourceId == sourceId, cancellationToken);

        public async Task<User[]> GetUsers(int skip, int take, CancellationToken cancellationToken)
            => await _users
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(cancellationToken);

        public async Task<User[]> GetUsers(Guid chatId, int skip, int take, CancellationToken cancellationToken)
            => await _messages.Where(m => m.ChatId == chatId)
                .GroupBy(m => m.UserId)
                .Select(m => m.Key)
                .Join(_users, userId => userId, user => user.Id, (userId, user) => user)
                .Skip(skip)
                .Take(take)
                .ToArrayAsync(cancellationToken);
    }
}
