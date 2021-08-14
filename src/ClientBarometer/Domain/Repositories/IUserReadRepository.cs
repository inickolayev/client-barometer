using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Repositories
{
    public interface IUserReadRepository
    {
        Task<User> Get(Guid userId, CancellationToken cancellationToken);
        Task<User> Get(string sourceId, CancellationToken cancellationToken);
        Task<bool> Contains(Guid userId, CancellationToken cancellationToken);
        Task<bool> Contains(string sourceId, CancellationToken cancellationToken);
        Task<User[]> GetUsers(int skip, int take, CancellationToken cancellationToken);
        Task<User[]> GetUsers(Guid chatId, CancellationToken cancellationToken);
    }
}