using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            var Keys = StockTableOps.SelectHistoricData("AAPL").Select(x => x.Date).ToList();
            foreach (var stock in StockList.SList)
            {
                var stockTempList = StockTableOps.SelectHistoricData(stock);
                if (stockTempList.Count < 15)
                {
                    foreach (var dateDiff in Keys.Except(stockTempList.Select(x=>x.Date).ToList()))
                    {
                        stockTempList.Add(new StockData()
                        {
                            Close = 0,
                            Date = dateDiff,
                            High = 0,
                            Low = 0,
                            NumberOfTransactions = 0,
                            Open = 0,
                            Ticker = stock,
                            Volume = 0
                        });
                    }
                }
                Console.WriteLine(stockTempList.Count);
            }
            foreach (var stock in StockList.SList)
            {
                var NewsTempList = NewsTwitterTableOps.SelectFromNewsTable(stock);
                if (NewsTempList.Count < 25)
                {
                    foreach (var dateDiff in Keys.Except(NewsTempList.Select(x=>x.Date).ToList()))
                    {
                        NewsTempList.Add(new NewsData()
                        {
                            Title = String.Empty,
                            Date = dateDiff,
                            Article_url = String.Empty,
                            Time = String.Empty,
                            Sentiment = 0,
                            Ticker = stock
                        });
                    }
                }
                Console.WriteLine(NewsTempList.Count);
            }
            Console.WriteLine("TestStr");
            await Task.Delay(10000, stoppingToken);
        }
    }
}