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
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //CreateTables.CreateHistoricStockTable();
                //Console.WriteLine("AloDa");
                //await HistoricStockData.UpdateStockData(stoppingToken, _logger);
                //await HistoricStockData.FirstStockDataRequest(stoppingToken, _logger);
                await Task.Delay(1000, stoppingToken);
                break;
            }
        }
    }
}