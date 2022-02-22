using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NewsTwitterDPM
{
    public class NewsData
    {
        public string Ticker { get; set; }
        public string Title { get; set; }
        public string Article_url { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public NewsData(string ticker, string title, string articleUrl, string date, string time)
        {
            Ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Article_url = articleUrl ?? throw new ArgumentNullException(nameof(articleUrl));
            Date = date;
            Time = time;
        }
    }

    
    // Historical is only 30 days
    // The model only takes in the latest 14 days
    public static class HistoricalNewsData
    {
        //public static string? RequestError;
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

        /// <summary>
        /// Lot of problems here
        /// For starters the code is copied from the stock data requesting methods
        /// Second at the moment considering the way that things are written we can't understand for which Ticker 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        ///
        public static async Task<Dictionary<string, string>> FirstNewsDataRequest(CancellationToken stoppingToken,
            ILogger logger)
        {
            var Date = DateOnly.FromDateTime(DateTime.Now);
            // Key in Dictionary is the name of the Ticker
            // Value is responseBody
            Dictionary<string, string> historicalData = new();
            string responseBody = String.Empty;
            BasicParse newsParse = new();
            // Makes sure connection is closed and httpClient is disposed of.
            using HttpClient httpClient = new();
            foreach (var stock in StockList.SList)
            {
                if (Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    Date = Date.AddDays(-1);
                    continue;
                }
                // Formats date to proper format
                string dateString = FormatDate(Date.AddDays(-DateTime.UtcNow.Day));
                try
                {
                    // Takes in Data for the last N days to the first day of the month
                    string httpUrl =
                        $"https://api.polygon.io/v2/reference/news?ticker={stock}&published_utc.gte={dateString}&limit=1000&apiKey=wfMvDI3LyIYWRUj0f7_kpLG1V_pmuy_w";
                    responseBody = await httpClient.GetStringAsync(httpUrl, stoppingToken);
                    // Since polygon.io doesn't give a mistake if you ask about data on a future date this statement makes sure that we get the right amount of data
                    if (newsParse.BasicNewsParseJson(responseBody) == 0)
                    {
                        logger.LogInformation("Non Data Response {Response}", responseBody);
                        logger.LogInformation("Problem At URL: {Url}", httpUrl);
                        logger.LogInformation("Status : {Status}", newsParse.RequestStatus);
                        if (newsParse.RequestError != null)
                        {
                            logger.LogInformation("Error : {Error}", newsParse.RequestError);
                        }

                        Date = Date.AddDays(-1);
                        await Task.Delay(12000, stoppingToken);
                        // If code goes in this block the continue statement will go to the next iteration
                        continue;
                    }

                    // Adding these curly brackets only for clarity , THEY ARE NOT NECESSARY
                    {
                        Console.WriteLine(responseBody);
                        logger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                        historicalData.Add(stock, responseBody);
                        Date = Date.AddDays(-1);
                        await Task.Delay(12000, stoppingToken);
                    }
                }
                catch (HttpRequestException e)
                {
                    logger.LogInformation("Caught exception: {Exception}", e.ToString());
                    await Task.Delay(12000, stoppingToken);
                }
            }
            Console.WriteLine("Bruh");
            return historicalData;
        }

        public static async Task<Dictionary<string, string>> UpdateNewsData(CancellationToken stoppingToken,
            ILogger logger, int nPast = 0)
        {
            // Today Date
            var Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-nPast));
            // Key in Dictionary is the name of the Ticker
            // Value is responseBody
            Dictionary<string, string> historicalData = new();
            string responseBody = String.Empty;
            BasicParse newsParse = new();
            // Makes sure connection is closed and httpClient is disposed of.
            using HttpClient httpClient = new();
            foreach (var stock in StockList.SList)
            {
                // Formats date to proper format
                string dateString = FormatDate(Date);
                try
                {
                    // Takes in Data for the last N days to the first day of the month
                    string httpUrl =
                        $"https://api.polygon.io/v2/reference/news?ticker={stock}&published_utc={dateString}&limit=100&apiKey=wfMvDI3LyIYWRUj0f7_kpLG1V_pmuy_w";
                    responseBody = await httpClient.GetStringAsync(httpUrl, stoppingToken);
                    Console.WriteLine(httpUrl);
                    // Since polygon.io doesn't give a mistake if you ask about data on a future date this statement makes sure that we get the right amount of data
                    if (newsParse.BasicNewsParseJson(responseBody) == 0)
                    {
                        logger.LogInformation("Non Data Response {Response}", responseBody);
                        logger.LogInformation("Problem At URL: {Url}", httpUrl);
                        logger.LogInformation("Status : {Status}", newsParse.RequestStatus);
                        if (newsParse.RequestError != null)
                        {
                            logger.LogInformation("Error : {Error}", newsParse.RequestError);
                        }

                        await Task.Delay(12000, stoppingToken);
                        // If code goes in this block the continue statement will go to the next iteration
                        continue;
                    }

                    Console.WriteLine(responseBody);
                    logger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                    historicalData.Add(stock, responseBody);
                    await Task.Delay(12000, stoppingToken);
                }
                catch (HttpRequestException e)
                {
                    logger.LogInformation("Caught exception: {Exception}", e.ToString());
                    await Task.Delay(12000, stoppingToken);
                }
                // TODO: Remove before commit
            }

            // Return dictionary of size 1 which is basically a KeyValuePair, but since c# doesn't implicitly
            // convert Dictionary to KeyValuePair and overloading seams ineffective anyway, just returning a List with
            // size one, which is not a problem so who gives a fuck.
            return historicalData;
        }

        public static List<NewsData> ParseNewsRequest(Dictionary<string, string> responseBody, ILogger logger)
        {
            List<NewsData> endResult = new();
            if (responseBody.Count == 0)
            {
                // Although possibly not the best solution takes care of any cases when there is an empty response(No result from the request)
                // The above described case is usually impossible but since the API has some undefined responses I put it here to be on the safe side
                // TODO: Find another solution instead of throwing exceptions
                throw new ArgumentNullException(nameof(responseBody), $"{nameof(responseBody)} is empty");
            }
            try
            {
                foreach (KeyValuePair<string, string> elem in responseBody)
                {
                    // Creates new stock instance and specifies the ticker
                    // Line #Possible Null Related Exception
                    dynamic jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(elem.Value);
                    List<Dictionary<string, JsonElement>> parsedData =
                        JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonData["results"]);
                    endResult = parsedData.Select(stock => new NewsData(
                        ticker: elem.Key,
                        title: stock["title"].GetString() ?? throw new InvalidOperationException(),
                        articleUrl: stock["article_url"].GetString() ?? throw new InvalidOperationException(),
                        //This should never be null so it's never a problem
                        date: stock["published_utc"].GetString().Split("T")[0] ?? throw new InvalidOperationException(),
                        time: stock["published_utc"].GetString().Split("T")[1] ?? throw new InvalidOperationException()
                    )).ToList();
                }
            }
            catch (Exception e)
            {
                logger.LogInformation("Exception thrown : {Exception}", e.ToString());
            }

            return endResult;
        }

        public static void PrettyPrint(int ElemsToPrint, List<NewsData> Data)
        {
            if (Data.Count <= ElemsToPrint)
            {
                for (int i = 1; i <= Data.Count; i++)
                {
                    Console.WriteLine(Data[i].Title);
                }
            }
            else
            {
                for (int i = 0; i < ElemsToPrint; i++)
                {
                    Console.WriteLine(Data[i].Title);
                }
            }
        }
    }
}