using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StockDPM
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
            int counter = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                if (counter == 0)
                {
                    CreateTables.CreateHistoricStockTable();
                }

                counter++;
            var PatternTestResult = await HistoricStockData.FirstStockDataRequest(stoppingToken, _logger);
            Console.WriteLine("fuck1");
            var test2 = HistoricStockData.ProcessStockData(PatternTestResult, _logger);
            Console.WriteLine("fuck2");
            CreateTables.BulkInsertHistoricStockTable(test2);
            
                Console.WriteLine("Fuck you");
                await Task.Delay(100000);
            }
        }
    }
}