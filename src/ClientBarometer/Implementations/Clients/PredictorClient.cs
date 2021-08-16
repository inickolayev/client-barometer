using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Contracts.Responses;
using ClientBarometer.Domain.Clients;
using ClientBarometer.Domain.Models;
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

        public async Task<OperationResult<PredictorResult>> SafeGetValue(GetPredictorRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var ans = await GetValue(request, cancellationToken);
                return new OperationResult<PredictorResult>(ans);
            }
            catch (Exception e)
            {
                return new OperationResult<PredictorResult>(e);
            }
        }
    }
}