using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Contracts.Responses;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Clients
{
    public interface IPredictorClient
    {
        Task<PredictorResult> GetValue(GetPredictorRequest request, CancellationToken cancellationToken);
        Task<OperationResult<PredictorResult>> SafeGetValue(GetPredictorRequest request, CancellationToken cancellationToken);
    }
}