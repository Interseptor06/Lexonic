using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NewsTwitterDPM
{

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private bool isInit = false;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (isInit == false)
                {
                    NewsTwitterTableOps.CreateNewsTable();
                }
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var test = await HistoricalNewsData.FirstNewsDataRequest(stoppingToken, _logger);
                var test2 = HistoricalNewsData.ParseNewsRequest(test, _logger);
                foreach (var elem in test2)
                {
                    NewsTwitterTableOps.InsertIntoNewsTable(elem);
                }
                
                Console.WriteLine("ALODA");
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}