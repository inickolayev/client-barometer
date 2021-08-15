using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Contracts.Responses;
using ClientBarometer.Domain.Clients;
using Flurl.Http;

namespace ClientBarometer.Implementations.Clients
{
    public class PredictorClient : IPredictorClient
    {
        private readonly IFlurlClient _client;
        
        public PredictorClient(HttpClient httpClient)
        {
            _client = new FlurlClient(httpClient);
        }

        public async Task<PredictorResult> GetValue(GetPredictorRequest request, CancellationToken cancellationToken)
            => await _client
                .Request("baro")
                .PostJsonAsync(request, cancellationToken)
                .ReceiveJson<PredictorResult>();
    }
}