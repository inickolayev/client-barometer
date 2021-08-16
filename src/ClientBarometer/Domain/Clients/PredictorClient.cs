using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Contracts.Responses;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Clients
{
    public interface IPredictorClient
    {
        Task<GetPredictorResult> GetValue(GetPredictorRequest request, CancellationToken cancellationToken);
        Task<OperationResult<GetPredictorResult>> SafeGetValue(GetPredictorRequest request, CancellationToken cancellationToken);
    }
}