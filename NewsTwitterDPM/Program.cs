using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NewsTwitterDPM;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<Worker>(); })
    .Build();

await host.RunAsync();
