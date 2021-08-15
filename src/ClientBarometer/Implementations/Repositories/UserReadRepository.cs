using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.DataAccess;
using ClientBarometer.Domain.Constants;
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

        public UserReadRepository(ClientBarometerDbContext dbContext)
        {
            _chats = dbContext.Chats.AsNoTracking();
            _messages = dbContext.Messages.AsNoTracking();
            _users = dbContext.Users.AsNoTracking();
        }
        
        public async Task<User> Get(Guid userId, CancellationToken cancellationToken)
            => await _users
            .FirstOrDefaultAsync(us => us.Id == userId, cancellationToken);

        public async Task<User> Get(string sourceId, CancellationToken cancellationToken)
            => await _users
                .FirstOrDefaultAsync(ch => ch.SourceId == sourceId, cancellationToken);

        public async Task<bool> Contains(Guid userId, CancellationToken cancellationToken)
            => await _users
                .AnyAsync(us => us.Id == userId, cancellationToken);
        
        public async Task<bool> Contains(string sourceId, CancellationToken cancellationToken)
            => await _users
                .AnyAsync(ch => ch.SourceId == sourceId, cancellationToken);

        public async Task<User[]> GetUsers(int skip, int take, CancellationToken cancellationToken)
            => await _users
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(cancellationToken);

        public async Task<User[]> GetUsers(Guid chatId, CancellationToken cancellationToken)
            => await _messages.Where(m => m.ChatId == chatId)
                .GroupBy(m => m.UserId)
                .Select(gr => gr.Key)
                .Join(_users, userId => userId, user => user.Id, (userId, user) => user)
                .ToArrayAsync(cancellationToken);
    }
}
