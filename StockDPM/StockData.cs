using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NewsTwitterDPM;
using NumSharp;

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
        public string Date { get; set; }
    }
    

    public static class HistoricStockData
    {
        // Had to create new method for the same thing as in NewsData.cs because the key is different
        
        /// <summary>
        /// Processes raw data
        /// </summary>
        /// <param name="responseBody"></param>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
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
                            NumberOfTransactions = stock["n"].GetInt64(),
                            Date = FormatDate(elem.Key)
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
            while (historicalData.Count < 15)
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
        /// <summary>
        /// Nearly same as above method, just recreated for updating, instead of getting data for last N days
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Utility method for file-based method
        /// </summary>
        /// <param name="response"></param>
        public static async Task WriteToFile(Dictionary<DateOnly,string> response)
        {
            foreach (KeyValuePair<DateOnly, string> Date in response)
            {
                string path = Path.Combine(Environment.CurrentDirectory, @"PolygonData", $"sta_{Date.Key.ToString().Replace("/", "-")}.json");
                await using FileStream fs = File.Create(path);
               fs.Close();
               await File.WriteAllTextAsync(path, Date.Value);
            }
        }
        
        /// <summary>
        /// Utility method for file-based method
        /// </summary>
        public static Dictionary<DateOnly, string> ReadFiles()
        {
            Dictionary<DateOnly, string> endResult = new();
            string path = Path.Combine(Environment.CurrentDirectory, @"Data",@"PolygonStockData");
            string[] files = Directory.GetFiles(path);
            foreach (string filename in files)
            {
                DateTime dt = DateTime.Parse(filename.Split("_")[1].Split(".")[0]);
                //Console.WriteLine(filename.Split("_")[1].Split(".")[0]);
                endResult.Add(DateOnly.FromDateTime(dt), File.ReadAllText(filename));
            }
            return endResult;
        }
        /// <summary>
        /// Inits data for stock prediction using ML model
        /// </summary>
        public static (NDArray, NDArray) InitAllData()
        {
            // Using Apple since they are a popular enough stock to avoid API-level errors
            var keyList = StockTableOps.SelectHistoricData("AAPL").OrderBy(x => x.Date).Select(x => x.Date).ToList();
            //keyList.ForEach(x => Console.WriteLine(x));
            List<List<List<double>>> stList = new();
            int counter = 0;
            foreach (var stock in StockList.SList)
            {
                var stockTempList = StockTableOps.SelectHistoricData(stock);
                if (stockTempList.Count < 15)
                {
                    foreach (var dateDiff in keyList.Except(stockTempList.Select(x=>x.Date).ToList()))
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
                stList.Add(new List<List<double>>());
                foreach (var elem in stockTempList)
                {
                    stList[counter].Add(new List<double>(){
                        double.Parse(elem.Open.ToString()),
                        double.Parse(elem.High.ToString()),
                        double.Parse(elem.Low.ToString()),
                        double.Parse(elem.Close.ToString()),
                        elem.Volume});
                }
                counter++;
            }


            List<List<List<double>>> nList = new();
            foreach (var stock in StockList.SList)
            {
                var NewsTempList = NewsTwitterTableOps.SelectFromNewsTable(stock);
                var groupedData = NewsTempList.OrderBy(x => x.Date).GroupBy(x => x.Date).ToList();
                var currentKeys = groupedData.Select(x => x.Key).ToList();
                List<List<NewsData>> fullList = new();
                foreach (var group in groupedData)
                {
                    var tempList = group.ToList();
                    while (tempList.Count < 25)
                    {
                        tempList.Add(new NewsData()
                        {
                            Title = String.Empty,
                            Date = group.Key,
                            Article_url = String.Empty,
                            Time = String.Empty,
                            Sentiment = 0,
                            Ticker = stock
                        });
                    }

                    // Truncation: 
                    if (tempList.Count > 25)
                    {
                        tempList = tempList.GetRange(0, 25);
                    }
                    fullList.Add(tempList);
                }

                foreach (var dateDiff in keyList.Except(currentKeys))
                {
                    var tempList = new List<NewsData>();
                    while (tempList.Count < 25)
                    {
                        tempList.Add(new NewsData()
                        {
                            Title = String.Empty,
                            Date = dateDiff,
                            Article_url = String.Empty,
                            Time = String.Empty,
                            Sentiment = 0,
                            Ticker = stock
                        });
                    }
                    fullList.Add(tempList);
                }
                fullList = fullList.OrderBy(x => x[0].Date).ToList();
                nList.Add(fullList.Select(a => a.Select(x => x.Sentiment).ToList()).ToList());

            }
            
            double[,,] ExchangeNews_X = new double[500, 15, 25];
            for (int i = 0; i < nList.Count; i++)
            {
                for (int j = 0; j < nList[0].Count; j++)
                {
                    for (int l = 0; l < nList[0][0].Count; l++)
                    {
                        ExchangeNews_X[i, j, l] = nList[i][j][l];
                    }
                }
            }
            double[,,] ExchangeStockData_X = new double[500, 15, 5];
            for (int i = 0; i < stList.Count; i++)
            {
                for (int j = 0; j < stList[0].Count; j++)
                {
                    for (int l = 0; l < stList[0][0].Count; l++)
                    {
                        ExchangeStockData_X[i, j, l] = stList[i][j][l];
                    }
                }
            }
            NDArray exchangeNewsX = ExchangeNews_X;
            NDArray exchangeStockDataX = ExchangeStockData_X;
            return (exchangeStockDataX, exchangeNewsX);
        }
        /// <summary>
        /// Inits prediction data
        /// </summary>
        public static void InitPredictionData()
        {
            StockTableOps.CreatePredictionDataTable();
            string path = Path.Combine(Environment.CurrentDirectory, @"PolygonStockData" , @"StockNPYs", @"EndAll.npy");
            var x = np.load(path);
            var Date = DateTime.Parse(Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"Data", @"PolygonStockData")).OrderByDescending(x => x)
                .ToList()[0].Split("_")[1].Split(".")[0]);
            Console.WriteLine(Date);
            for (int i = 0; i < 500; i++)
            {
                StockTableOps.InsertPredictionDataTable(StockList.SList[i],Date, x.ToArray<float>()[i]);
            }
        }

        /// <summary>
        /// Benchmark for model results
        /// </summary>
        public static void TestModel()
        {
            var T_Data = File.ReadAllText(@"/home/martin/RiderProjects/Lexonic/StockDPM/TestingData/TestTomorrowData.json");
            var N_Data = File.ReadAllText(@"/home/martin/RiderProjects/Lexonic/StockDPM/PolygonData/sta_3-8-2022.json");
            var processedT_Data = ProcessStockData(new Dictionary<DateOnly, string>() {{DateOnly.FromDateTime(DateTime.Now), T_Data}}, null).First().Value.ToList();
            var processedN_Data = ProcessStockData(new Dictionary<DateOnly, string>() {{DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), N_Data}}, null).First().Value.ToList();
            var predictionData = StockTableOps.SelectPredictionData();
            var dic = StockList.SList.Zip(predictionData, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            double profit = 0;
            foreach (string stock in StockList.SList)
            {
                if (processedT_Data.Where(x => x.Ticker == stock).ToList().Count == 0 ||
                    processedN_Data.Where(x => x.Ticker == stock).ToList().Count == 0)
                {
                    continue;
                }
                var currentTData = processedT_Data.Where(x => x.Ticker == stock).ToList()[0];
                var currentNData = processedN_Data.Where(x => x.Ticker == stock).ToList()[0];
                var tempDiff = currentTData.Close - currentNData.Close;
                if (dic[stock] == "Hold")
                {
                    profit += 50 * decimal.ToDouble(tempDiff);
                }
                else if (dic[stock] == "Buy")
                {
                    profit += 100 * decimal.ToDouble(tempDiff);
                }
                else
                {
                    profit += 50 * decimal.ToDouble(tempDiff) * -1;
                }
                Console.WriteLine(tempDiff);
                Console.WriteLine(profit);
            }
            Console.WriteLine(profit);
        }

        public static void InitDataAndWriteToDb()
        {
            StockTableOps.CreateHistoricStockTable();
            var Data = ReadFiles();
            var ProcessedData = ProcessStockData(Data, null);
            var StockLists = ProcessedData.Values.SelectMany(x => x).ToList();
            StockTableOps.BulkInsertHistoricStockTable(StockLists);
            // Init Prediction Data - Already Written Method to be DRY
            InitPredictionData();
        }
    }
}
