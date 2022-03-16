using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NumSharp;


namespace NewsTwitterDPM
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //var y = HistoricalNewsData.Init();
                //Console.WriteLine(y[0].Title);
                Console.WriteLine("TestStr");
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
