// See https://aka.ms/new-console-template for more information

using System;
using StockDPM;
using NewsTwitterDPM;

/*
 * Simple, but tests nearly every component of the project
 */

Console.WriteLine("Choose Option");
Console.WriteLine("1. Init Stock Data");
Console.WriteLine("2. News Data");
var result = int.Parse(Console.ReadLine());
if (result == 1)
{
    HistoricStockData.InitDataAndWriteToDb();
}
else if (result == 2)
{
    HistoricalNewsData.Init();
}
else
{
    Console.WriteLine("Something Went Wrong!");
}