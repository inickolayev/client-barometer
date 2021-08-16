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

        public async Task<GetPredictorResult> GetValue(GetPredictorRequest request, CancellationToken cancellationToken)
            => await _client
                .Request("baro")
                .PostJsonAsync(request, cancellationToken)
                .ReceiveJson<GetPredictorResult>();

        public async Task<OperationResult<GetPredictorResult>> SafeGetValue(GetPredictorRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var ans = await GetValue(request, cancellationToken);
                return new OperationResult<GetPredictorResult>(ans);
            }
            catch (Exception e)
            {
                return new OperationResult<GetPredictorResult>(e);
            }
        }
    }
}