using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsTwitterDPM;

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
            /*
            //var ExchangeData_x = await HistoricStockData.FirstStockDataRequest(stoppingToken, _logger);
            //await HistoricStockData.WriteToFile(ExchangeData_x);
            //NewsTwitterTableOps.SelectFromNewsTable(StockList.SList[0]).ForEach(x=> Console.WriteLine(x.Date));
            var newsData = NewsTwitterDPM.NewsTwitterTableOps.SelectFromNewsTable("AAPL");
            var x = HistoricStockData.ReadFiles();
            var y = HistoricStockData.ProcessStockData(x, null);
            var n = y.Values.SelectMany(z=> z).ToList();
            StockDPM.CreateTables.BulkInsertHistoricStockTable(n);
            //var grouped  = n.GroupBy(b => b.Date).ToList();
            //Console.WriteLine(grouped[0].Key);
            Console.WriteLine(x.Values.Count);
            */
            Console.WriteLine(StockTableOps.SelectHistoricData("AAPL")[0].Close);
            
            Console.WriteLine(StockList.SList.Count);
            Console.WriteLine("TestStr");
            await Task.Delay(10000, stoppingToken);
        }
    }
}