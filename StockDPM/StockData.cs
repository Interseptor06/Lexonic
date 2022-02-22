using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace StockDPM
{
    public class StockData
    {
        public string Ticker { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public Int64 Volume { get; set; }
        public Int64 NumberOfTransactions { get; set; }
    }
    

    public static class HistoricStockData
    {
        // Had to create new method for the same thing as in NewsData.cs because the key is different
        

        // TODO: Add commentary before I forget how the fuck this method works
        public static Dictionary<DateOnly, List<StockData>> ProcessStockData(Dictionary<DateOnly, string> responseBody, ILogger logger)
        {
            Dictionary<DateOnly, List<StockData>> globalHistoricData = new();
            try
            {
                if (responseBody.Count == 0)
                {
                    // TODO: Find another solution instead of throwing exceptions
                    throw new ArgumentNullException(nameof(responseBody), $"{nameof(responseBody)} is empty");
                }
                Dictionary<DateOnly, List<StockData>> historicData = new();
                foreach (KeyValuePair<DateOnly, string> elem in responseBody)
                {
                    dynamic jsonData = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(elem.Value);
                    List<Dictionary<string, JsonElement>> parsedData =
                        JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonData["results"]);
                    List<StockData> timestepData = parsedData
                        .Where(stock => StockList.SList.Contains(stock["T"].ToString())).Select(stock => new StockData()
                        {
                            High = stock["h"].GetDecimal(),
                            Low = stock["l"].GetDecimal(),
                            Open = stock["o"].GetDecimal(),
                            Close = stock["c"].GetDecimal(),
                            Ticker = stock["T"].GetString(),
                            Volume = Int64.Parse(stock["v"].GetDecimal().ToString()),
                            NumberOfTransactions = stock["n"].GetInt64()
                        })
                        .ToList();

                    historicData.Add(elem.Key, timestepData);
                }
                globalHistoricData = historicData;
            }
            catch (Exception e)
            {
                logger.LogInformation("Exception thrown : {Exception}", e.ToString());
            }
            // TODO: Check if it's ok to return things like this
            return globalHistoricData;
        }

        // Utility function for processing the Date to a format that the API will take
        public static string FormatDate(DateOnly _dateOnly)
        {
            string dateString = String.Empty;
            if (_dateOnly.ToString().Split("/")[0].Length == 1)
            {
                if (_dateOnly.ToString().Split("/")[1].Length == 1)
                {
                    dateString = _dateOnly.ToString().Split("/")[2] + "-0" + _dateOnly.ToString().Split("/")[0] + "-0" + _dateOnly.ToString().Split("/")[1];
                }
                else
                {
                    dateString = _dateOnly.ToString().Split("/")[2] + "-0" + _dateOnly.ToString().Split("/")[0] + "-" + _dateOnly.ToString().Split("/")[1];
                }
            }
            else
            {
                if (_dateOnly.ToString().Split("/")[1].Length == 1)
                {
                    dateString = _dateOnly.ToString().Split("/")[2] + "-" + _dateOnly.ToString().Split("/")[0] + "-0" + _dateOnly.ToString().Split("/")[1];

                }
                else
                {
                    dateString = _dateOnly.ToString().Split("/")[2] + "-" + _dateOnly.ToString().Split("/")[0] + "-" + _dateOnly.ToString().Split("/")[1];
                }
            }
            return dateString;
        }
        
        // Fills Table with one year of stock data
        // Logs info about every request 
        public static async Task<Dictionary<DateOnly,string>> FirstStockDataRequest(CancellationToken stoppingToken, ILogger logger)
        {
            var Date = DateOnly.FromDateTime(DateTime.Now);
            Dictionary<DateOnly, string> historicalData = new();
            string responseBody = String.Empty;
            BasicParse stockParse = new();
            HttpClient httpClient = new();
            while (historicalData.Count <= 253)
            {
                
                if (Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    Date = Date.AddDays(-1);
                    continue;
                }
                // Formats date to proper format
                string dateString = FormatDate(Date);
                try
                {
                    responseBody = await httpClient.GetStringAsync(
                        $"https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/{dateString}?adjusted=true&apiKey=wfMvDI3LyIYWRUj0f7_kpLG1V_pmuy_w", stoppingToken);
                    Console.WriteLine($"https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/{dateString}?adjusted=true&apiKey=wfMvDI3LyIYWRUj0f7_kpLG1V_pmuy_w");
                    // Since polygon.io doesn't give a mistake if you ask about data on a (weekend, holiday, future date) this statement makes sure that we get the right amount of data
                    if (stockParse.BasicStockParseJson(responseBody) == 0)
                    {
                        logger.LogInformation("Non Data Response {Response}",responseBody);
                        logger.LogInformation("Status : {Status}", stockParse.RequestStatus);
                        if(stockParse.RequestError != null){ logger.LogInformation("Error : {Error}", stockParse.RequestError); }
                        await Task.Delay(12000, stoppingToken);
                        Date = Date.AddDays(-1);
                        continue;
                    }
                    // Put in else statement for clarity
                    // Continue statement takes care of everything else so no need of else statement
                    logger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                    historicalData.Add(Date, responseBody);
                    Date = Date.AddDays(-1);
                    await Task.Delay(12000, stoppingToken);
                }
                catch (Exception e)
                {
                    logger.LogInformation("Exception thrown : {Exception}", e.ToString());
                    await Task.Delay(12000, stoppingToken);
                }
                Console.WriteLine(historicalData.Count);
            }

            return historicalData;
        }
        
        public static async Task<Dictionary<DateOnly, string>> UpdateStockData(CancellationToken stoppingToken, ILogger logger)
        {
            Dictionary<DateOnly, string> historicalData = new();
            string responseBody = String.Empty;
            BasicParse stockParse = new();
            HttpClient httpClient = new();
            
            // This section handles Date formatting
            var Date = DateOnly.FromDateTime(DateTime.Now);
            // Formats date to proper format
            string dateString = FormatDate(Date);
            // Handles Http Request
            try
            {
                responseBody = await httpClient.GetStringAsync(
                    $"https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/{dateString}?adjusted=true&apiKey=wfMvDI3LyIYWRUj0f7_kpLG1V_pmuy_w", stoppingToken);
                // Since polygon.io doesn't give a mistake if you ask about data on a (weekend, holiday, future date) this statement makes sure that we get the right amount of data
                if (stockParse.BasicStockParseJson(responseBody) == 0)
                {
                    logger.LogInformation("Non Data Response {Response}",responseBody);
                    logger.LogInformation("Status : {Status}", stockParse.RequestStatus);
                    if(stockParse.RequestError != null){ logger.LogInformation("Error : {Error}", stockParse.RequestError); }
                }
                else
                {
                    logger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow); 
                    historicalData.Add(Date,responseBody);
                }
                await Task.Delay(12000, stoppingToken);
            }
            catch (HttpRequestException e)
            {
                logger.LogInformation("Exception thrown : {Exception}", e.ToString());
                await Task.Delay(12000, stoppingToken);
            }
            // End Result is Dictionary with size 1 which is basically a KeyValuePair 
            // This way the same function for processing the historical data can be used in this case
            return historicalData;
        }
        
    }
}
