using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Contracts;
using ClientBarometer.Contracts.Requests;
using ClientBarometer.Contracts.Responses;
using ClientBarometer.Domain.Constants;
using ClientBarometer.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientBarometer.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("info")]
        public async Task<PersonalInfo> GetInfo(Guid userId, CancellationToken cancellationToken)
            => await _userService.GetInfo(userId, cancellationToken);
    }
}
