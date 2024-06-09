using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Services
{
    public class TimedCryptoUpdateService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedCryptoUpdateService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer _timer;

        public TimedCryptoUpdateService(ILogger<TimedCryptoUpdateService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TimedCryptoUpdateService is running..");

            //run every 2 minutes
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10000));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Crypto Update Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var cryptoUpdateService = scope.ServiceProvider.GetRequiredService<ICryptoUpdateService>();
                try
                {
                    cryptoUpdateService.UpdateCryptoCurrenciesAsync().Wait();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred when updating crypto currencies.");
                }
            }
        }
    }
}
