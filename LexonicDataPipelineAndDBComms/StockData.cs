using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace LexonicDataPipelineAndDBComms
{
    public class StockData
    {
        public string Ticker { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public UInt64 Volume { get; set; }
        public UInt64 NumberOfTransactions { get; set; }
    }

    public static class HistoricStockData
    {
        public static Dictionary<DateOnly, List<StockData>> ProcessStockData(Dictionary<DateOnly,string> responseBody)
        { 
            Dictionary<DateOnly, List<StockData>> historicData = new();
            foreach (KeyValuePair<DateOnly, string> elem in responseBody)
            {
                dynamic jsonData = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(elem.Value);
                List<Dictionary<string,JsonElement>> parsedData = JsonSerializer.Deserialize<List<Dictionary<string,JsonElement>>>(jsonData["results"]);
                List<StockData> timestepData = parsedData.Where(stock => StockList.SList.Contains(stock["T"].ToString() ?? throw new InvalidOperationException())).Select(stock => new StockData()
                    {
                        High = stock["h"].GetDouble(),
                        Low = stock["l"].GetDouble(),
                        Open = stock["o"].GetDouble(),
                        Close = stock["c"].GetDouble(),
                        Ticker = stock["T"].GetString() ?? throw new InvalidOperationException(),
                        Volume = Decimal.ToUInt64(stock["v"].GetDecimal()),
                        NumberOfTransactions = stock["n"].GetUInt64()
                    })
                    .ToList();

                historicData.Add(elem.Key, timestepData);
            }
            return historicData;
        }

        public static async Task<Dictionary<DateOnly,string>> GetStockData(CancellationToken stoppingToken)
        {
            /////////////// Has To change Later on in development before production release
            // If not fixed data will not be up to date
            // var Date = DateOnly.FromDateTime(DateTime.Now);
            var Date = DateOnly.FromDateTime(DateTime.Now).AddDays(-1);
            Dictionary<DateOnly, string> historicalData = new();
            string responseBody = String.Empty;
            HttpClient httpClient = new();
            while (historicalData.Count < 1)
            {
                string dateString = String.Empty;
                if (Date.ToString().Split("/")[0].Length == 1)
                {
                    if (Date.ToString().Split("/")[1].Length == 1)
                    {
                        dateString = Date.ToString().Split("/")[2] + "-0" + Date.ToString().Split("/")[0] + "-0" + Date.ToString().Split("/")[1];
                    }
                    else
                    {
                        dateString = Date.ToString().Split("/")[2] + "-0" + Date.ToString().Split("/")[0] + "-" + Date.ToString().Split("/")[1];
                    }
                }
                else
                {
                    if (Date.ToString().Split("/")[1].Length == 1)
                    {
                        dateString = Date.ToString().Split("/")[2] + "-" + Date.ToString().Split("/")[0] + "-0" + Date.ToString().Split("/")[1];

                    }
                    else
                    {
                        dateString = Date.ToString().Split("/")[2] + "-" + Date.ToString().Split("/")[0] + "-" + Date.ToString().Split("/")[1];
                    }
                } 
                
                try
                {
                    responseBody = await httpClient.GetStringAsync(
                        $"https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/{dateString}?adjusted=true&apiKey=wfMvDI3LyIYWRUj0f7_kpLG1V_pmuy_w", stoppingToken);
                    historicalData.Add(Date,responseBody);
                    Date = Date.AddDays(-1);
                    await Task.Delay(1000, stoppingToken);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Got Exception");
                    Console.WriteLine(e.Message);
                    await Task.Delay(12000, stoppingToken);
                }
            }
            return historicalData;
        }

        public static void WriteToDb(Dictionary<DateOnly,string> data)
        {
            // Implement after setup of mssql container
        }
    }
}
