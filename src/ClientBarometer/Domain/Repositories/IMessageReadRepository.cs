using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Repositories
{
    public interface IMessageReadRepository
    {
        Task<Message> Get(Guid messageId, CancellationToken cancellationToken);
        Task<Message[]> GetMessages(Guid chatId, int skip, int take, CancellationToken cancellationToken);
        Task<Message[]> GetLastMessages(Guid chatId, int takeLast, CancellationToken cancellationToken);

    }
}