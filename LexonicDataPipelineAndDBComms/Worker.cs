using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LexonicDataPipelineAndDBComms
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
            HttpClient httpClient = new HttpClient();
            while (!stoppingToken.IsCancellationRequested)
            {
                try	
                {
                    /*
                    HttpResponseMessage response = await httpClient.GetAsync("http://www.contoso.com/");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    */
                    // Above three lines can be replaced with new helper method below
                    string responseBody = await httpClient.GetStringAsync("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=IBM&apikey=demo");
                    dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(responseBody);
                    Console.WriteLine(json_data);
                    _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now); 
                    await Task.Delay(1000, stoppingToken);
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");	
                    Console.WriteLine("Message :{0} ",e.Message);
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}