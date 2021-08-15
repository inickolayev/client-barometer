using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Repositories
{
    public interface IBarometerReadRepository
    {
        Task<BarometerEntry> Get(Guid chatId, CancellationToken cancellationToken);
        Task<bool> Contains(Guid chatId, CancellationToken cancellationToken);
    }
}