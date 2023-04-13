using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Evento.Infrastructure.Services
{
    public class PasswordTokenValidityService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PasswordTokenValidityService> _logger;
        private Timer? _timer = null;

        public PasswordTokenValidityService(ILogger<PasswordTokenValidityService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider; 
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            // To nie działa, problem z operacjami wielowątkowymi oraz EntityFramework
            // TODO: zmiennić obsługę
            using (var scope = _serviceProvider.CreateScope())
            {
                var userService =scope.ServiceProvider.GetRequiredService<IUserService>();
                await userService.PasswordTokenValidate();
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
