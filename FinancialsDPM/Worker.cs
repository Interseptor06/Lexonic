using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using FinancialsDPM.FinancialsDPM;
using FinancialsDPM.FinancialsDPM.FinancialsDPM;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GetBalanceSheetData = FinancialsDPM.FinancialsDPM.GetBalanceSheetData;

namespace FinancialsDPM
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


                Console.WriteLine("Manqk");
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(50000, stoppingToken);
            }
        }
    }
}