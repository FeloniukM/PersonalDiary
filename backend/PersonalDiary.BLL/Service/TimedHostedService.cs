using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalDiary.BLL.Interfaces;

namespace PersonalDiary.BLL.Service
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly IProceduresService _proceduresService;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer? _timer = null;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory factory, IProceduresService proceduresService)
        {
            _logger = logger;
            _proceduresService = proceduresService;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(30));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            _proceduresService.DeleteNonRestoredUsers();

            _logger.LogInformation(
                "Timed Hosted Service is working");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
