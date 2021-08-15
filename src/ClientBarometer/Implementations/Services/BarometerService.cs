using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Domain.Clients;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using ClientBarometer.Domain.Services;
using ClientBarometer.Domain.UnitsOfWork;
using Microsoft.Extensions.Caching.Memory;
using Requests = ClientBarometer.Contracts.Requests;
using Responses = ClientBarometer.Contracts.Responses;

namespace ClientBarometer.Implementations.Services
{
    public class BarometerService : IBarometerService
    {
        private readonly IMessageReadRepository _messageReadRepository;
        private readonly IBarometerReadRepository _barometerReadRepository;
        private readonly IPredictorClient _predictorClient;
        private readonly IChatUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public BarometerService(IMessageReadRepository messageReadRepository,
            IBarometerReadRepository barometerReadRepository,
            IPredictorClient predictorClient,
            IChatUnitOfWork unitOfWork,
            IMemoryCache memoryCache)
        {
            _messageReadRepository = messageReadRepository;
            _barometerReadRepository = barometerReadRepository;
            _predictorClient = predictorClient;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        public async Task<Responses.BarometerValue> GetValue(Guid chatId, CancellationToken cancellationToken)
        {
            int prevValue = 0;
            if (await _barometerReadRepository.Contains(chatId, cancellationToken))
            {
                prevValue = (await _barometerReadRepository.Get(chatId, cancellationToken)).Value;
            }
            var request = await GetPredictorRequest(chatId, prevValue, cancellationToken);
            if (!string.IsNullOrEmpty(request.CustomerFollowingMessage))
            {
                var result = await _memoryCache.GetOrCreateAsync(GetKey(request), async entry =>
                {
                    var newResult = await _predictorClient.GetValue(request, cancellationToken);
                    var newValue = (int) (newResult.Result * 1000);
                    await CreateOrUpdate(chatId, newValue, cancellationToken);
                    return new Responses.BarometerValue
                    {
                        Value = newValue
                    };
                });
                return result;
            }
            else
            {
                return new Responses.BarometerValue
                {
                    Value = prevValue,
                };
            }
        }
        
        private async Task CreateOrUpdate(Guid chatId, int newValue, CancellationToken cancellationToken)
        {
            if (!await _barometerReadRepository.Contains(chatId, cancellationToken))
            {
                _unitOfWork.BarometerValues.RegisterNew(new BarometerEntry
                {
                    ChatId = chatId,
                    Value = newValue
                });
            }
            else
            {
                var found = await _barometerReadRepository.Get(chatId, cancellationToken);
                found.Value = newValue;
                _unitOfWork.BarometerValues.RegisterDirty(found);
            }
            await _unitOfWork.Complete(cancellationToken);
        }

        private async Task<GetPredictorRequest> GetPredictorRequest(Guid chatId, int prevValue, CancellationToken cancellationToken)
        {
            var messages = (await _messageReadRepository
                .GetMessages(chatId, 0, ChatConsts.MESSAGES_TAKE_DEFAULT, cancellationToken))
                .ToArray();
            var request = new GetPredictorRequest
            {
                PrevBaro = Convert.ToDouble(prevValue) / 1000,
            };
            
            int count = 0;
            string currentMessage = "";
            Guid currentUserId = Guid.NewGuid();
            foreach (var message in messages)
            {
                if (currentUserId != message.UserId)
                {
                    count++;
                    
                    if (count == 2)
                    {
                        if (currentUserId == ChatConsts.DEFAULT_USER_ID)
                        {
                            request.SellerAnswer = currentMessage;
                        }
                        else
                        {
                            request.CustomerFollowingMessage = currentMessage;
                        }
                    }
                    else if (count == 3)
                    {
                        if (currentUserId == ChatConsts.DEFAULT_USER_ID)
                        {
                            request.SellerAnswer = currentMessage;
                        }
                        else
                        {
                            request.CustomerInitMessage = currentMessage;
                        }
                    }
                    else if (count == 4)
                    {
                        if (currentUserId != ChatConsts.DEFAULT_USER_ID)
                        {
                            request.CustomerInitMessage = currentMessage;
                        }
                        break;
                    }
                    
                    currentUserId = message.UserId;
                    currentMessage = message.Text;
                }
                else
                {
                    currentMessage = string.Join(" ", new[] {currentMessage, message.Text});
                }
            }

            return request;
        }

        private string GetKey(GetPredictorRequest request)
            => string.Join(" ",
                new[]
                {
                    request.PrevBaro.ToString(),
                    request.CustomerInitMessage,
                    request.SellerAnswer,
                    request.CustomerFollowingMessage
                });
    }
}