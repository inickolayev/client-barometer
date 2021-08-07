using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Repositories
{
    public interface IChatReadRepository
    {
        Task<Chat> Get(Guid chatId, CancellationToken cancellationToken);
        Task<bool> Contains(Guid chatId, CancellationToken cancellationToken);
        Task<bool> ContainsBySource(Guid sourceId, CancellationToken cancellationToken);
        Task<Chat[]> GetChats(Guid chatId, int skip, int take, CancellationToken cancellationToken);
    }
}