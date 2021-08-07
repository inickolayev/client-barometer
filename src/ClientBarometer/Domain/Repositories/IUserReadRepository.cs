using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Repositories
{
    public interface IUserReadRepository
    {
        Task<User> Get(Guid userId, CancellationToken cancellationToken);
        Task<bool> Contains(Guid userId, CancellationToken cancellationToken);
        Task<bool> ContainsBySource(Guid sourceId, CancellationToken cancellationToken);
        Task<User[]> GetUsers(int skip, int take, CancellationToken cancellationToken);
        Task<User[]> GetUsers(Guid chatId, int skip, int take, CancellationToken cancellationToken);
    }
}