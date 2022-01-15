using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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
                            High = stock["h"].GetDouble(),
                            Low = stock["l"].GetDouble(),
                            Open = stock["o"].GetDouble(),
                            Close = stock["c"].GetDouble(),
                            Ticker = stock["T"].GetString(),
                            Volume = Decimal.ToUInt64(stock["v"].GetDecimal()),
                            NumberOfTransactions = stock["n"].GetUInt64()
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
            HttpClient httpClient = new();
            while (historicalData.Count <= 253)
            {
                // Formats date to proper format
                string dateString = FormatDate(Date);
                try
                {
                    responseBody = await httpClient.GetStringAsync(
                        $"https://api.polygon.io/v2/aggs/grouped/locale/us/market/stocks/{dateString}?adjusted=true&apiKey=wfMvDI3LyIYWRUj0f7_kpLG1V_pmuy_w", stoppingToken);
                    // Since polygon.io doesn't give a mistake if you ask about data on a (weekend, holiday, future date) this statement makes sure that we get the right amount of data
                    if (responseBody.Length < 250)
                    {
                        logger.LogInformation("Non Data Response {Response}",responseBody);
                        continue;
                    }
                    // Put in else statement for clarity
                    else
                    {
                        // Continue statement takes care of everything else so no need of else statement
                        logger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                        historicalData.Add(Date, responseBody);
                    }
                    Date = Date.AddDays(-1);
                    await Task.Delay(12000, stoppingToken);
                }
                catch (HttpRequestException e)
                {
                    logger.LogInformation("Exception thrown : {Exception}", e.ToString());
                    await Task.Delay(12000, stoppingToken);
                }
            }
            return historicalData;
        }
        
        public static async Task<Dictionary<DateOnly, string>> UpdateStockData(CancellationToken stoppingToken, ILogger logger)
        {
            Dictionary<DateOnly, string> historicalData = new();
            string responseBody = String.Empty;
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
                if (responseBody.Length < 250)
                {
                    logger.LogInformation("Non Data Response {Response}",responseBody);
                }
                else
                {
                    logger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow); 
                    historicalData.Add(Date,responseBody);
                }
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
        
        public static async Task FillTable(string data)
        {
            // TODO: Create method that creates (login,user,role) for each service(Twitter, News, Ticker Data etc.) that will be accessing the DB
            // For now it is left empty 
            string Password = String.Empty;
            string Id = String.Empty;
            string connStr = $"Server=localhost;database=testDB;User ID={Id}; Password={Password}; Encrypt=No;Initial Catalog=TestDB";
            await using (SqlConnection myConn = new(connStr))
            { 
                myConn.Open();
                // Do work here //
                Console.WriteLine("Opened");
                // Connection closed here //
            } 
            await Task.Delay(5000);
        }
    }
}
