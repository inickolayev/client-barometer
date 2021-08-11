using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ngrok.Adapter.Service;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ClientBarometer.Implementations.Services
{
    public class TelegramBotInitService : IHostedService
    {
        private readonly IServiceProvider _service;
        private readonly INgrokService _ngrokService;

        public TelegramBotInitService(IServiceProvider service, INgrokService ngrokService)
        {
            _service = service;
            _ngrokService = ngrokService;
        }

        public async Task StartAsync(CancellationToken token)
        {
            var tunnelUrl = await _ngrokService.GetHttpsTunnelUrl();
            var ngrokUrl = $"{tunnelUrl}/api/TelegramBot";
            using (var scope = _service.CreateScope())
            {
                var client = scope.ServiceProvider.GetService<TelegramBotClient>();
                await client.SetWebhookAsync(ngrokUrl, cancellationToken: token);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
