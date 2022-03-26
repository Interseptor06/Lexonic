using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsTwitterDPM;
using StockDPM;
using FinancialsDPM;
using Microsoft.Extensions.Logging;




var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

Console.WriteLine("Start/Pre-HistoricalNewsInit");
HistoricalNewsData.Init();
Console.WriteLine("Post-HistoricalNewsInit/Pre-StockDataInit");
HistoricStockData.InitDataAndWriteToDb();
Console.WriteLine("Post-StockInit/PreCompanyOverviewInit");
GetCompanyOverviewData.InitData();
Console.WriteLine("Done");

app.Run();
