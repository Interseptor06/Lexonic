using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsTwitterDPM;
using NumSharp;
using NumSharp.Utilities;

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


            //var (exchangeStockDataX, exchangeNewsX) = HistoricStockData.InitAllData();
            
            //Console.WriteLine(exchangeNewsX.Shape);
            //Console.WriteLine(exchangeStockDataX.Shape);
            //np.Save((Array)exchangeStockDataX, @"/home/martin/RiderProjects/Lexonic/StockDPM/NPYs/ExchangeStockX.npy");
            //np.Save((Array)exchangeNewsX, @"/home/martin/RiderProjects/Lexonic/StockDPM/NPYs/ExchangeNewsX.npy");
            HistoricStockData.TestModel();
            
            Console.WriteLine("TestStr");
            await Task.Delay(10000, stoppingToken);
        }
    }
}