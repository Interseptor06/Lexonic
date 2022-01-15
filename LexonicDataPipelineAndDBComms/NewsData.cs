using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LexonicDataPipelineAndDBComms
{
    public class NewsData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Article_url { get; set; }

        public NewsData(string _title, string _description, string _articleUrl)
        {
            Title = _title;
            Description = _description;
            Article_url = _articleUrl;
        }
    }

    // Historical is only 30 days
    // The model only takes in the latest 14 days
    public class HistoricalNewsData
    {
        public static async Task<Dictionary<DateOnly,string>> FirstNewsDataRequest(CancellationToken stoppingToken, ILogger logger)
        {
            var Date = DateOnly.FromDateTime(DateTime.Now);
            Dictionary<DateOnly, string> historicalData = new();
            string responseBody = String.Empty;
            HttpClient httpClient = new();
            foreach (var stock in StockList.SList)
            {
                while (historicalData.Count <= DateTime.UtcNow.Day) {
                    // Formats date to proper format
                    string dateString = HistoricStockData.FormatDate(Date);
                    try
                    {
                        // Problematic
                        responseBody = await httpClient.GetStringAsync(
                            $"https://api.polygon.io/v2/reference/news?ticker={stock}&published_utc={dateString}&limit=100&apiKey=wfMvDI3LyIYWRUj0f7_kpLG1V_pmuy_w",
                            stoppingToken);
                        // Since polygon.io doesn't give a mistake if you ask about data on a future date this statement makes sure that we get the right amount of data
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
                        await Task.Delay(1000, stoppingToken);
                    }
                    catch (HttpRequestException e)
                    {
                        logger.LogInformation("Caught exception: {Exception}", e.ToString());
                        await Task.Delay(12000, stoppingToken);
                    }
                }
            }

            return historicalData;
        }
    }
    
}