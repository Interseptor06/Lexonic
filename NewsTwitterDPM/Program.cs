/*using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NewsTwitterDPM;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<Worker>(); })
    .Build();

await host.RunAsync();
*/
using NewsTwitterDPM;

NewsData data = new NewsData("AAPL", "AloDaTest", "www.google.com", "2022-02-01", "10:00");
//CreateTables.CreateNewsTable();
CreateTables.InsertIntoNewsTable(data);
CreateTables.InsertIntoNewsTable(data);
CreateTables.InsertIntoNewsTable(data);
CreateTables.InsertIntoNewsTable(data);
CreateTables.InsertIntoNewsTable(data);

