using System;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Models;
using Requests = ClientBarometer.Contracts.Requests;
using Responses = ClientBarometer.Contracts.Responses;

namespace ClientBarometer.Domain.Services
{
    public interface IUserService
    {
        Task<Responses.PersonalInfo> GetInfo(Guid userId, CancellationToken cancellationToken);
    }
}