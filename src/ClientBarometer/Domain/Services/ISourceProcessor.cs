using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Services
{
    public interface ISourceProcessor
    {
        Task ProcessToSource(CreateMessageRequest message, CancellationToken cancellationToken);
        Task ProcessFromSource(CreateMessageRequest message, CancellationToken cancellationToken);
    }
}