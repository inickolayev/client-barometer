using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Domain.Repositories;
using ClientBarometer.Domain.Services;
using ClientBarometer.Implementations.Exceptions;
using Requests = ClientBarometer.Contracts.Requests;
using Responses = ClientBarometer.Contracts.Responses;

namespace ClientBarometer.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReadRepository _userReadRepository;

        public UserService(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }
        
        public async Task<Responses.PersonalInfo> GetInfo(Guid userId, CancellationToken cancellationToken)
        {
            if (!await _userReadRepository.Contains(userId, cancellationToken))
            {
                throw new UserNotFoundException(userId.ToString());
            }
            var user = await _userReadRepository.Get(userId, cancellationToken);
            return new Responses.PersonalInfo
            {
                Id = user.Id,
                Username = user.SourceId,
                Name = user.Name,
                Age = DateTime.Now.Year - user.Birthday.Year,
            };
        }
    }
}