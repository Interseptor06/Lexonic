using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LexonicDataPipelineAndDBComms
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
            // This method is probably going to be the last thing that is setup in this project
            // The MLModels project will have to be finished first
            // After that the DB communication will be finished
            // Only when all of the above is finished can we proceed to the development of the web application
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.UtcNow);
                await HistoricalNewsData.FirstNewsDataRequest(stoppingToken, _logger);
                //await HistoricStockData.UpdateStockData(stoppingToken, _logger);
                //await HistoricStockData.WriteToDb("Test");
                //await Task.Delay(1000, stoppingToken);
            }
        }
    }
}