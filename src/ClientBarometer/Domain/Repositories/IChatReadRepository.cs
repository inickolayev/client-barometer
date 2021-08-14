using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Repositories
{
    public interface IChatReadRepository
    {
        Task<Chat> Get(Guid chatId, CancellationToken cancellationToken);
        Task<Chat> Get(string sourceId, CancellationToken cancellationToken);
        Task<bool> Contains(Guid chatId, CancellationToken cancellationToken);
        Task<bool> Contains(string sourceId, CancellationToken cancellationToken);
        Task<Chat[]> GetChats(int skip, int take, CancellationToken cancellationToken);
    }
}