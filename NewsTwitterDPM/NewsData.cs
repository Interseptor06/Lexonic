using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NumSharp;


namespace NewsTwitterDPM
{
    public class NewsData
    {
        /// <summary>
        /// Class is for data encapsulation
        /// Constructors are for ease of use and null exception safety when possible, although we do provide a default constructor
        /// </summary>
        public string Ticker { get; set; }
        public string Title { get; set; }
        public string Article_url { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double Sentiment { get; set; }
        //Default constructor
        public NewsData()
        {
            //Default
        }
        public NewsData(string ticker, string title, string articleUrl, string date, string time)
        {
            Ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Article_url = articleUrl ?? throw new ArgumentNullException(nameof(articleUrl));
            Date = date;
            Time = time;
        }
        public NewsData(string ticker, string title, string articleUrl, string date, string time, double sentiment)
        {
            Ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Article_url = articleUrl ?? throw new ArgumentNullException(nameof(articleUrl));
            Date = date;
            Time = time;
            Sentiment = sentiment;
        }
    }
    public class ParseOnlyNewsData
    {
        /// <summary>
        /// Since the API returns a list of Tickers for each article this is a temporary class used ONLY during data
        /// processing and not anywhere else.
        /// The only difference is that instead of a Ticker we have a TickerList which is an array
        /// </summary>
        public string[] TickerList { get; set; }
        public string Title { get; set; }
        public string Article_url { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public ParseOnlyNewsData(string[] tickerlist, string title, string articleUrl, string date, string time)
        {
            TickerList = tickerlist ?? throw new ArgumentNullException(nameof(tickerlist));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Article_url = articleUrl ?? throw new ArgumentNullException(nameof(articleUrl));
            Date = date;
            Time = time;
        }
    }
    
    // Historical is only 15 days
    // The model only takes in the latest 14 days
    public static class HistoricalNewsData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dateOnly"></param>
        /// <returns> A string, which is a date, formatted for the API</returns>
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
        /// File based approach, take news data foreach date in StockDPM/PolygonData and get articles
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <param name="logger"></param>
        /// <returns> Dictionary{Date, responseBody} </returns>
        ///
        public static async Task<Dictionary<string, string>> FirstNewsDataRequest(CancellationToken stoppingToken,
            ILogger logger)
        {
            // Key in Dictionary is the date
            // Value is responseBody
            Dictionary<string, string> historicalData = new();
            string responseBody = String.Empty;
            BasicParse newsParse = new();
            // Makes sure connection is closed and httpClient is disposed of.
            using HttpClient httpClient = new();
            string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, @"StockDPM", @"PolygonData");
            string[] files = Directory.GetFiles(path);
            foreach (var filename in files)
            {
                string month = filename.Split("_")[1].Split("-")[0];
                string day = filename.Split("_")[1].Split("-")[1];
                if (day.Length == 1) { day = "0" + day; }
                if (month.Length == 1) { month = "0" + month; }
                // Formats date to proper format
                string date = filename.Split("_")[1].Split("-")[2].Split(".")[0] + "-" + month + "-" + day;
                try
                {
                    // Gets data from the same date as the stock data
                    string httpUrl =
                        $"https://api.polygon.io/v2/reference/news?published_utc={date}&limit=1000&apiKey=wfMvDI3LyIYWRUj0f7_kpLG1V_pmuy_w";
                    Console.WriteLine(httpUrl);
                    responseBody = await httpClient.GetStringAsync(httpUrl, stoppingToken);
                    Console.WriteLine(responseBody.Length);
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
                    
                        //Console.WriteLine(responseBody);
                        logger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                        historicalData.Add(date, responseBody);
                        await Task.Delay(12000, stoppingToken);
                    
                }
                catch (HttpRequestException e)
                {
                    logger.LogInformation("Caught exception: {Exception}", e.ToString());
                    await Task.Delay(12000, stoppingToken);
                }
            }
            return historicalData;
        }
        /// <summary>
        /// Method will be used when we get full access to API but instead of getting historical data, it just
        /// makes a request foreach stock every day, once a day.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <param name="logger"></param>
        /// <param name="nPast"> For when the market hasn't closed and you don't want to waste requests</param>
        /// <returns>Same data as method above, for ease of parsing later on -> Dictionary{date, responseBody}</returns>
        public static async Task<Dictionary<string, string>> UpdateNewsData(CancellationToken stoppingToken,
            ILogger logger = null, int nPast = 0)
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

                    //Console.WriteLine(responseBody);
                    logger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                    historicalData.Add(stock, responseBody);
                    await Task.Delay(12000, stoppingToken);
                }
                catch (HttpRequestException e)
                {
                    logger.LogInformation("Caught exception: {Exception}", e.ToString());
                    await Task.Delay(12000, stoppingToken);
                }
            }   

            // Return dictionary of size 1 which is basically a KeyValuePair, but since c# doesn't implicitly
            // convert Dictionary to KeyValuePair and overloading seams ineffective anyway, just returning a List with
            // size one, which is not a problem so who gives a fuck.
            return historicalData;
        }
        /// <summary>
        /// Processes above data to a List of NewsData
        /// </summary>
        /// <param name="responseBody"></param>
        /// <param name="logger"></param>
        /// <returns>List of NewsData</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static List<NewsData> ParseNewsRequest(Dictionary<string, string> responseBody, ILogger logger)
        {
            List<NewsData> endResult = new();
            List<List<ParseOnlyNewsData>> endTempResult = new();
            try
            {
                foreach (KeyValuePair<string, string> elem in responseBody)
                {
                    // Creates new stock instance and specifies the ticker
                    dynamic jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(elem.Value);
                    List<Dictionary<string, JsonElement>> parsedData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonData["results"]);
                    endTempResult.Add(parsedData.Select(stock => new ParseOnlyNewsData(
                        tickerlist: Array.ConvertAll(stock["tickers"].EnumerateArray().ToArray(), title => title.GetString() ?? throw new NullReferenceException()),
                        title: stock["title"].GetString() ?? throw new InvalidOperationException(),
                        articleUrl: stock["article_url"].GetString() ?? throw new InvalidOperationException(),
                        //This should never be null so it's never a problem
                        date: stock["published_utc"].GetString().Split("T")[0] ?? throw new InvalidOperationException(),
                        time: stock["published_utc"].GetString().Split("T")[1] ?? throw new InvalidOperationException()
                    )).ToList());
                }
                
                var newTempList = endTempResult.SelectMany(x => x).ToList();
                Console.WriteLine(newTempList.Count);
                foreach (var tempresult in newTempList)
                {
                    foreach (var ticker in tempresult.TickerList)
                    {
                        if (StockList.SList.Contains(ticker))
                        {
                            endResult.Add(new NewsData(
                                ticker: ticker,
                                title: tempresult.Title,
                                articleUrl: tempresult.Article_url,
                                date: tempresult.Date,
                                time: tempresult.Time));
                        }
                    }
                }

            }
            catch (Exception e)
            {
                logger.LogInformation("Exception thrown : {Exception}", e.ToString());
            }

            return endResult;
        }
        
        /// <summary>
        /// Utility method for new file-based approach
        /// </summary>
        /// <param name="response"></param>
        public static async Task WriteToFile(Dictionary<string, string> response)
        {
            foreach (KeyValuePair<string, string> Date in response)
            {
                string path = Path.Combine(Environment.CurrentDirectory, @"PolygonData", $"snd_{Date.Key}.json");
                await using FileStream fs = File.Create(path);
                fs.Close();
                await File.WriteAllTextAsync(path, Date.Value);
            }
        }
        /// <summary>
        /// Utility method for new file-based approach
        /// </summary>
        public static Dictionary<string, string> ReadFiles()
        {
            Dictionary<string, string> endResult = new();
            string path = Path.Combine(Environment.CurrentDirectory, @"PolygonData");
            string[] files = Directory.GetFiles(path);
            foreach (string filename in files)
            {
                //Console.WriteLine(filename.Split("_")[1].Split(".")[0]);
                endResult.Add(filename.Split("_")[1].Split(".")[0], File.ReadAllText(filename));
            }
            return endResult;
        }
        /// <summary>
        /// Utility method to Initialize Data
        /// </summary>
        public static List<NewsData> Init()
        {
            var allData = HistoricalNewsData.ReadFiles();
            Console.WriteLine(allData.Keys.Count);
            var ParsedData = HistoricalNewsData.ParseNewsRequest(allData, null);
            var y = ParsedData.Select(data => data.Title).ToList();
            var xNdArray =
                np.load("/home/martin/RiderProjects/Lexonic/NewsTwitterDPM/SentimentNPYs/CurrentNewsSentiment.npy");

            for (int i = 0; i < (xNdArray.Shape[0] & ParsedData.Count); i++)
            {
                ParsedData[i].Sentiment = xNdArray.ToArray<double>()[i];
            }
            NewsTwitterTableOps.CreateNewsTable();
            foreach (var newsData in ParsedData)
            {
                NewsTwitterTableOps.InsertIntoNewsTable(newsData);
                //Console.WriteLine(newsData.Date);
            }
            return ParsedData;
        }
    }
}