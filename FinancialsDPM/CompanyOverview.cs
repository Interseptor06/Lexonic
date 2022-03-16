using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FinancialsDPM
{
    namespace FinancialsDPM
    {



        namespace FinancialsDPM
        {
            /// <summary>
            /// Constructor is mainly for Null Exception Safety.
            /// Class is for data encapsulation.
            /// </summary>
            public class CompanyOverview
            {
                public string Ticker;
                public string AssetType;
                public string Name;
                public string Description;
                public string CIK;
                public string Exchange;
                public string Currency;
                public string Country;
                public string Sector;
                public string Industry;
                public string Address;
                public string FiscalYearEnd;
                public string LatestQuarter;
                public string MarketCapitalization;
                public string EBITDA;
                public string PERatio;
                public string PEGRatio;
                public string BookValue;
                public string DividendPerShare;
                public string DividendYield;
                public string EPS;
                public string RevenuePerShareTTM;
                public string ProfitMargin;
                public string OperatingMarginTTM;
                public string ReturnOnAssetsTTM;
                public string ReturnOnEquityTTM;
                public string RevenueTTM;
                public string GrossProfitTTM;
                public string DilutedEPSTTM;
                public string QuarterlyEarningsGrowthYOY;
                public string QuarterlyRevenueGrowthYOY;
                public string AnalystTargetPrice;
                public string TrailingPE;
                public string ForwardPE;
                public string PriceToSalesRatioTTM;
                public string PriceToBookRatio;
                public string EVToRevenue;
                public string EVToEBITDA;
                public string Beta;
                public string FiftyTwoWeekHigh;
                public string FiftyTwoWeekLow;
                public string FiftyDayMovingAverage;
                public string TwoHundredDayMovingAverage;
                public string SharesOutstanding;
                public string DividendDate;
                public string ExDividendDate;

                public CompanyOverview(string ticker, string assetType, string name, string description, string cik,
                    string exchange, string currency, string country, string sector, string industry, string address,
                    string fiscalYearEnd, string latestQuarter, string marketCapitalization, string ebitda,
                    string peRatio,
                    string pegRatio, string bookValue, string dividendPerShare, string dividendYield, string eps,
                    string revenuePerShareTtm, string profitMargin, string operatingMarginTtm, string returnOnAssetsTtm,
                    string returnOnEquityTtm, string revenueTtm, string grossProfitTtm, string dilutedEpsttm,
                    string quarterlyEarningsGrowthYoy, string quarterlyRevenueGrowthYoy, string analystTargetPrice,
                    string trailingPe, string forwardPe, string priceToSalesRatioTtm, string priceToBookRatio,
                    string evToRevenue, string evToEbitda, string beta, string fiftyTwoWeekHigh, string fiftyTwoWeekLow,
                    string fiftyDayMovingAverage, string twoHundredDayMovingAverage, string sharesOutstanding,
                    string dividendDate, string exDividendDate)
                {
                    Ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
                    AssetType = assetType ?? throw new ArgumentNullException(nameof(assetType));
                    Name = name ?? throw new ArgumentNullException(nameof(name));
                    Description = description ?? throw new ArgumentNullException(nameof(description));
                    CIK = cik ?? throw new ArgumentNullException(nameof(cik));
                    Exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
                    Currency = currency ?? throw new ArgumentNullException(nameof(currency));
                    Country = country ?? throw new ArgumentNullException(nameof(country));
                    Sector = sector ?? throw new ArgumentNullException(nameof(sector));
                    Industry = industry ?? throw new ArgumentNullException(nameof(industry));
                    Address = address ?? throw new ArgumentNullException(nameof(address));
                    FiscalYearEnd = fiscalYearEnd ?? throw new ArgumentNullException(nameof(fiscalYearEnd));
                    LatestQuarter = latestQuarter ?? throw new ArgumentNullException(nameof(latestQuarter));
                    MarketCapitalization =
                        marketCapitalization ?? throw new ArgumentNullException(nameof(marketCapitalization));
                    EBITDA = ebitda ?? throw new ArgumentNullException(nameof(ebitda));
                    PERatio = peRatio ?? throw new ArgumentNullException(nameof(peRatio));
                    PEGRatio = pegRatio ?? throw new ArgumentNullException(nameof(pegRatio));
                    BookValue = bookValue ?? throw new ArgumentNullException(nameof(bookValue));
                    DividendPerShare = dividendPerShare ?? throw new ArgumentNullException(nameof(dividendPerShare));
                    DividendYield = dividendYield ?? throw new ArgumentNullException(nameof(dividendYield));
                    EPS = eps ?? throw new ArgumentNullException(nameof(eps));
                    RevenuePerShareTTM =
                        revenuePerShareTtm ?? throw new ArgumentNullException(nameof(revenuePerShareTtm));
                    ProfitMargin = profitMargin ?? throw new ArgumentNullException(nameof(profitMargin));
                    OperatingMarginTTM =
                        operatingMarginTtm ?? throw new ArgumentNullException(nameof(operatingMarginTtm));
                    ReturnOnAssetsTTM = returnOnAssetsTtm ?? throw new ArgumentNullException(nameof(returnOnAssetsTtm));
                    ReturnOnEquityTTM = returnOnEquityTtm ?? throw new ArgumentNullException(nameof(returnOnEquityTtm));
                    RevenueTTM = revenueTtm ?? throw new ArgumentNullException(nameof(revenueTtm));
                    GrossProfitTTM = grossProfitTtm ?? throw new ArgumentNullException(nameof(grossProfitTtm));
                    DilutedEPSTTM = dilutedEpsttm ?? throw new ArgumentNullException(nameof(dilutedEpsttm));
                    QuarterlyEarningsGrowthYOY = quarterlyEarningsGrowthYoy ??
                                                 throw new ArgumentNullException(nameof(quarterlyEarningsGrowthYoy));
                    QuarterlyRevenueGrowthYOY = quarterlyRevenueGrowthYoy ??
                                                throw new ArgumentNullException(nameof(quarterlyRevenueGrowthYoy));
                    AnalystTargetPrice =
                        analystTargetPrice ?? throw new ArgumentNullException(nameof(analystTargetPrice));
                    TrailingPE = trailingPe ?? throw new ArgumentNullException(nameof(trailingPe));
                    ForwardPE = forwardPe ?? throw new ArgumentNullException(nameof(forwardPe));
                    PriceToSalesRatioTTM =
                        priceToSalesRatioTtm ?? throw new ArgumentNullException(nameof(priceToSalesRatioTtm));
                    PriceToBookRatio = priceToBookRatio ?? throw new ArgumentNullException(nameof(priceToBookRatio));
                    EVToRevenue = evToRevenue ?? throw new ArgumentNullException(nameof(evToRevenue));
                    EVToEBITDA = evToEbitda ?? throw new ArgumentNullException(nameof(evToEbitda));
                    Beta = beta ?? throw new ArgumentNullException(nameof(beta));
                    FiftyTwoWeekHigh = fiftyTwoWeekHigh ?? throw new ArgumentNullException(nameof(fiftyTwoWeekHigh));
                    FiftyTwoWeekLow = fiftyTwoWeekLow ?? throw new ArgumentNullException(nameof(fiftyTwoWeekLow));
                    FiftyDayMovingAverage = fiftyDayMovingAverage ??
                                            throw new ArgumentNullException(nameof(fiftyDayMovingAverage));
                    TwoHundredDayMovingAverage = twoHundredDayMovingAverage ??
                                                 throw new ArgumentNullException(nameof(twoHundredDayMovingAverage));
                    SharesOutstanding = sharesOutstanding ?? throw new ArgumentNullException(nameof(sharesOutstanding));
                    DividendDate = dividendDate ?? throw new ArgumentNullException(nameof(dividendDate));
                    ExDividendDate = exDividendDate ?? throw new ArgumentNullException(nameof(exDividendDate));
                }

                public CompanyOverview(string ticker)
                {
                    Ticker = ticker;
                }
            }

            public static class GetCompanyOverviewData
            {
                private static string api_key = "47BGPYJPFN4CEC20";
                /// <summary>
                /// Requests data for each ticker in StockList.SList
                /// </summary>
                /// <param name="ilogger"></param>
                /// <param name="stoppingToken"></param>
                /// <param name="toBreak"></param>
                /// <returns> List of API responses </returns>
                public static async Task<List<string>> CompanyOverviewRequest(string Ticker, ILogger ilogger,
                    CancellationToken stoppingToken)
                {
                    List<string> CompanyOverviews = new();
                    using HttpClient httpClient = new();
                    string responseBody = string.Empty;

                    try
                    {
                        responseBody = await httpClient.GetStringAsync(
                            $"https://www.alphavantage.co/query?function=OVERVIEW&symbol={Ticker}&apikey={api_key}",
                            stoppingToken);
                        ilogger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                        //await Task.Delay(12000, stoppingToken);
                    }
                    catch (Exception e)
                    {
                        ilogger.LogInformation("Exception thrown : {Exception}", e.ToString());
                        //await Task.Delay(12000, stoppingToken);
                    }

                    CompanyOverviews.Add(responseBody);


                    return CompanyOverviews;
                }
                /// <summary>
                /// Processes the above gotten data.
                /// </summary>
                /// <param name="dList"></param>
                /// <returns>List of Company Overview Classes for each stock in StockList.SList</returns>
                /// <exception cref="InvalidOperationException"></exception>
                public static List<CompanyOverview> ProcessCompanyOverviewData(List<string> dList)
                {
                    List<CompanyOverview> parsedData = new();
                    foreach (string response in dList)
                    {
                        var Data = JsonSerializer.Deserialize<Dictionary<string, string>>(response) ??
                                   throw new InvalidOperationException();
                        string tempTicker = Data["Symbol"];

                        string AssetType = Data["AssetType"];
                        string tempName = Data["Name"];
                        string tempDescription = Data["Description"];
                        string tempCIK = Data["CIK"];
                        string tempExchange = Data["Exchange"];
                        string tempCurrency = Data["Currency"];
                        string tempCountry = Data["Country"];
                        string tempSector = Data["Sector"];
                        string tempIndustry = Data["Industry"];
                        string tempAddress = Data["Address"];
                        string tempFiscalYearEnd = Data["FiscalYearEnd"];
                        string tempLatestQuarter = Data["LatestQuarter"];
                        string tempMarketCapitalization = Data["MarketCapitalization"];
                        string tempEBITDA = Data["EBITDA"];
                        string tempPERatio = Data["PERatio"];
                        string tempPEGRatio = Data["PEGRatio"];
                        string tempBookValue = Data["BookValue"];
                        string tempDividendPerShare = Data["DividendPerShare"];
                        string tempDividendYield = Data["DividendYield"];
                        string tempEPS = Data["EPS"];
                        string tempRevenuePerShareTTM = Data["RevenuePerShareTTM"];
                        string tempProfitMargin = Data["ProfitMargin"];
                        string tempOperatingMarginTTM = Data["OperatingMarginTTM"];
                        string tempReturnOnAssetsTTM = Data["ReturnOnAssetsTTM"];
                        string tempReturnOnEquityTTM = Data["ReturnOnEquityTTM"];
                        string tempRevenueTTM = Data["RevenueTTM"];
                        string tempGrossProfitTTM = Data["GrossProfitTTM"];
                        string tempDilutedEPSTTM = Data["DilutedEPSTTM"];
                        string tempQuarterlyEarningsGrowthYOY = Data["QuarterlyEarningsGrowthYOY"];
                        string tempQuarterlyRevenueGrowthYOY = Data["QuarterlyRevenueGrowthYOY"];
                        string tempAnalystTargetPrice = Data["AnalystTargetPrice"];
                        string tempTrailingPE = Data["TrailingPE"];
                        string tempForwardPE = Data["ForwardPE"];
                        string tempPriceToSalesRatioTTM = Data["PriceToSalesRatioTTM"];
                        string tempPriceToBookRatio = Data["PriceToBookRatio"];
                        string tempEVToRevenue = Data["EVToRevenue"];
                        string tempEVToEBITDA = Data["EVToEBITDA"];
                        string tempBeta = Data["Beta"];
                        string tempFiftyTwoWeekHigh = Data["52WeekHigh"];
                        string tempFiftyTwoWeekLow = Data["52WeekLow"];
                        string tempFiftyDayMovingAverage = Data["50DayMovingAverage"];
                        string tempTwoHundredDayMovingAverage = Data["200DayMovingAverage"];
                        string tempSharesOutstanding = Data["SharesOutstanding"];
                        string tempDividendDate = Data["DividendDate"];
                        string tempExDividendDate = Data["ExDividendDate"];

                        CompanyOverview currentStock = new(tempTicker,
                            AssetType,
                            tempName,
                            tempDescription,
                            tempCIK,
                            tempExchange,
                            tempCurrency,
                            tempCountry,
                            tempSector,
                            tempIndustry,
                            tempAddress,
                            tempFiscalYearEnd,
                            tempLatestQuarter,
                            tempMarketCapitalization,
                            tempEBITDA,
                            tempPERatio,
                            tempPEGRatio,
                            tempBookValue,
                            tempDividendPerShare,
                            tempDividendYield,
                            tempEPS,
                            tempRevenuePerShareTTM,
                            tempProfitMargin,
                            tempOperatingMarginTTM,
                            tempReturnOnAssetsTTM,
                            tempReturnOnEquityTTM,
                            tempRevenueTTM,
                            tempGrossProfitTTM,
                            tempDilutedEPSTTM,
                            tempQuarterlyEarningsGrowthYOY,
                            tempQuarterlyRevenueGrowthYOY,
                            tempAnalystTargetPrice,
                            tempTrailingPE,
                            tempForwardPE,
                            tempPriceToSalesRatioTTM,
                            tempPriceToBookRatio,
                            tempEVToRevenue,
                            tempEVToEBITDA,
                            tempBeta,
                            tempFiftyTwoWeekHigh,
                            tempFiftyTwoWeekLow,
                            tempFiftyDayMovingAverage,
                            tempTwoHundredDayMovingAverage,
                            tempSharesOutstanding,
                            tempDividendDate,
                            tempExDividendDate
                        );
                        parsedData.Add(currentStock);
                    }

                    return parsedData;
                }
            }
        }
    }
}