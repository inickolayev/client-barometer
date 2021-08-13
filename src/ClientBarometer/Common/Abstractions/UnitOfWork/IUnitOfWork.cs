using System.Threading;
using System.Threading.Tasks;

namespace ClientBarometer.Common.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Complete(CancellationToken ct = default);
    }
}