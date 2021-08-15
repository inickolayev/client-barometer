using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using Requests = ClientBarometer.Contracts.Requests;
using Responses = ClientBarometer.Contracts.Responses;

namespace ClientBarometer.Domain.Services
{
    public class SuggestionService : ISuggestionService
    {
        private readonly IMessageReadRepository _messageReadRepository;

        public SuggestionService(IMessageReadRepository messageReadRepository)
        {
            _messageReadRepository = messageReadRepository;
        }

        public static string[] Suggestions = new[]
        {
            "Давайте я вам помогу?",
            "Не хотите узнать о других предложениях?",
            "Предлагаю узнать о новых поступлениях",
            "Как я могу к вам обращаться?",
            "Есть ли какие-нибудь вопросы?",
        };
        
        public async Task<Responses.Suggestions> GetSuggestions(Guid chatId, CancellationToken cancellationToken)
        {
            var messages = await _messageReadRepository.GetMessages(chatId, 0, int.MaxValue, cancellationToken);
            var suggestions = Suggestions.Where((sg, i) => messages.Length % 2 == i % 2).ToArray();
            return new Responses.Suggestions
            {
                Messages = suggestions
            };
        }
    }
}