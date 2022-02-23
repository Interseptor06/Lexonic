namespace FinancialsDPM
{
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
        public class BalanceSheet
        {
            public string Ticker;
            public string fiscalDateEnding;
            public string reportedCurrency;
            public string totalAssets;
            public string totalCurrentAssets;
            public string cashAndCashEquivalentsAtCarryingValue;
            public string cashAndShortTermInvestments;
            public string inventory;
            public string currentNetReceivables;
            public string totalNonCurrentAssets;
            public string propertyPlantEquipment;
            public string accumulatedDepreciationAmortizationPPE;
            public string intangibleAssets;
            public string intangibleAssetsExcludingGoodwill;
            public string goodwill;
            public string investments;
            public string longTermInvestments;
            public string shortTermInvestments;
            public string otherCurrentAssets;
            public string otherNonCurrrentAssets;
            public string totalLiabilities;
            public string totalCurrentLiabilities;
            public string currentAccountsPayable;
            public string deferredRevenue;
            public string currentDebt;
            public string shortTermDebt;
            public string totalNonCurrentLiabilities;
            public string capitalLeaseObligations;
            public string longTermDebt;
            public string currentLongTermDebt;
            public string longTermDebtNoncurrent;
            public string shortLongTermDebtTotal;
            public string otherCurrentLiabilities;
            public string otherNonCurrentLiabilities;
            public string totalShareholderEquity;
            public string treasuryStock;
            public string retainedEarnings;
            public string commonStock;
            public string commonStockSharesOutstanding;

            public BalanceSheet(string ticker ,string fiscalDateEnding, string reportedCurrency, string totalAssets,
                string totalCurrentAssets, string cashAndCashEquivalentsAtCarryingValue,
                string cashAndShortTermInvestments,
                string inventory, string currentNetReceivables, string totalNonCurrentAssets,
                string propertyPlantEquipment,
                string accumulatedDepreciationAmortizationPpe, string intangibleAssets,
                string intangibleAssetsExcludingGoodwill, string goodwill, string investments,
                string longTermInvestments,
                string shortTermInvestments, string otherCurrentAssets, string otherNonCurrrentAssets,
                string totalLiabilities, string totalCurrentLiabilities, string currentAccountsPayable,
                string deferredRevenue, string currentDebt, string shortTermDebt, string totalNonCurrentLiabilities,
                string capitalLeaseObligations, string longTermDebt, string currentLongTermDebt,
                string longTermDebtNoncurrent, string shortLongTermDebtTotal, string otherCurrentLiabilities,
                string otherNonCurrentLiabilities, string totalShareholderEquity, string treasuryStock,
                string retainedEarnings, string commonStock, string commonStockSharesOutstanding)
            {
                Ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
                this.fiscalDateEnding = fiscalDateEnding ?? throw new ArgumentNullException(nameof(fiscalDateEnding));
                this.reportedCurrency = reportedCurrency ?? throw new ArgumentNullException(nameof(reportedCurrency));
                this.totalAssets = totalAssets ?? throw new ArgumentNullException(nameof(totalAssets));
                this.totalCurrentAssets =
                    totalCurrentAssets ?? throw new ArgumentNullException(nameof(totalCurrentAssets));
                this.cashAndCashEquivalentsAtCarryingValue = cashAndCashEquivalentsAtCarryingValue ??
                                                             throw new ArgumentNullException(
                                                                 nameof(cashAndCashEquivalentsAtCarryingValue));
                this.cashAndShortTermInvestments = cashAndShortTermInvestments ??
                                                   throw new ArgumentNullException(nameof(cashAndShortTermInvestments));
                this.inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
                this.currentNetReceivables =
                    currentNetReceivables ?? throw new ArgumentNullException(nameof(currentNetReceivables));
                this.totalNonCurrentAssets =
                    totalNonCurrentAssets ?? throw new ArgumentNullException(nameof(totalNonCurrentAssets));
                this.propertyPlantEquipment = propertyPlantEquipment ??
                                              throw new ArgumentNullException(nameof(propertyPlantEquipment));
                accumulatedDepreciationAmortizationPPE = accumulatedDepreciationAmortizationPpe ??
                                                         throw new ArgumentNullException(
                                                             nameof(accumulatedDepreciationAmortizationPpe));
                this.intangibleAssets = intangibleAssets ?? throw new ArgumentNullException(nameof(intangibleAssets));
                this.intangibleAssetsExcludingGoodwill = intangibleAssetsExcludingGoodwill ??
                                                         throw new ArgumentNullException(
                                                             nameof(intangibleAssetsExcludingGoodwill));
                this.goodwill = goodwill ?? throw new ArgumentNullException(nameof(goodwill));
                this.investments = investments ?? throw new ArgumentNullException(nameof(investments));
                this.longTermInvestments =
                    longTermInvestments ?? throw new ArgumentNullException(nameof(longTermInvestments));
                this.shortTermInvestments =
                    shortTermInvestments ?? throw new ArgumentNullException(nameof(shortTermInvestments));
                this.otherCurrentAssets =
                    otherCurrentAssets ?? throw new ArgumentNullException(nameof(otherCurrentAssets));
                this.otherNonCurrrentAssets = otherNonCurrrentAssets ??
                                              throw new ArgumentNullException(nameof(otherNonCurrrentAssets));
                this.totalLiabilities = totalLiabilities ?? throw new ArgumentNullException(nameof(totalLiabilities));
                this.totalCurrentLiabilities = totalCurrentLiabilities ??
                                               throw new ArgumentNullException(nameof(totalCurrentLiabilities));
                this.currentAccountsPayable = currentAccountsPayable ??
                                              throw new ArgumentNullException(nameof(currentAccountsPayable));
                this.deferredRevenue = deferredRevenue ?? throw new ArgumentNullException(nameof(deferredRevenue));
                this.currentDebt = currentDebt ?? throw new ArgumentNullException(nameof(currentDebt));
                this.shortTermDebt = shortTermDebt ?? throw new ArgumentNullException(nameof(shortTermDebt));
                this.totalNonCurrentLiabilities = totalNonCurrentLiabilities ??
                                                  throw new ArgumentNullException(nameof(totalNonCurrentLiabilities));
                this.capitalLeaseObligations = capitalLeaseObligations ??
                                               throw new ArgumentNullException(nameof(capitalLeaseObligations));
                this.longTermDebt = longTermDebt ?? throw new ArgumentNullException(nameof(longTermDebt));
                this.currentLongTermDebt =
                    currentLongTermDebt ?? throw new ArgumentNullException(nameof(currentLongTermDebt));
                this.longTermDebtNoncurrent = longTermDebtNoncurrent ??
                                              throw new ArgumentNullException(nameof(longTermDebtNoncurrent));
                this.shortLongTermDebtTotal = shortLongTermDebtTotal ??
                                              throw new ArgumentNullException(nameof(shortLongTermDebtTotal));
                this.otherCurrentLiabilities = otherCurrentLiabilities ??
                                               throw new ArgumentNullException(nameof(otherCurrentLiabilities));
                this.otherNonCurrentLiabilities = otherNonCurrentLiabilities ??
                                                  throw new ArgumentNullException(nameof(otherNonCurrentLiabilities));
                this.totalShareholderEquity = totalShareholderEquity ??
                                              throw new ArgumentNullException(nameof(totalShareholderEquity));
                this.treasuryStock = treasuryStock ?? throw new ArgumentNullException(nameof(treasuryStock));
                this.retainedEarnings = retainedEarnings ?? throw new ArgumentNullException(nameof(retainedEarnings));
                this.commonStock = commonStock ?? throw new ArgumentNullException(nameof(commonStock));
                this.commonStockSharesOutstanding = commonStockSharesOutstanding ??
                                                    throw new ArgumentNullException(
                                                        nameof(commonStockSharesOutstanding));
            }

            public BalanceSheet(string ticker)
            {
                Ticker = ticker;
            }
        }

        public static class GetBalanceSheetData
        {
            private static string api_key = "47BGPYJPFN4CEC20";

            public static async Task<List<string>> BalanceSheetRequest(ILogger ilogger, CancellationToken stoppingToken, bool toBreak=false)
            {
                List<string> BalanceSheets = new();
                using HttpClient httpClient = new();
                string responseBody = string.Empty;
                foreach (string stock in StockList.SList)
                {
                    try
                    {
                        responseBody = await httpClient.GetStringAsync(
                            $"https://www.alphavantage.co/query?function=BALANCE_SHEET&symbol={stock}&apikey={api_key}",
                            stoppingToken);
                        ilogger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                        await Task.Delay(12000, stoppingToken);
                    }
                    catch (Exception e)
                    {
                        ilogger.LogInformation("Exception thrown : {Exception}", e.ToString());
                        await Task.Delay(12000, stoppingToken);
                    }

                    BalanceSheets.Add(responseBody);
                    if (toBreak)
                    {
                        break;
                    }
                }

                return BalanceSheets;
            }

            public static List<BalanceSheet> ProcessBalanceSheetData(List<string> dList)
            {
                List<BalanceSheet> parsedData = new();
                foreach (string response in dList)
                {
                    var jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(response) ??
                                   throw new InvalidOperationException();
                    string tempTicker = jsonData["symbol"].GetString();
                    var Data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonData["annualReports"]);

                    string tempFiscalDateEnding = Data[0]["fiscalDateEnding"];
                    string tempReportedCurrency = Data[0]["reportedCurrency"];
                    string tempTotalAssets = Data[0]["totalAssets"];
                    string tempTotalCurrentAssets = Data[0]["totalCurrentAssets"];
                    string tempCashAndCashEquivalentsAtCarryingValue = Data[0]["cashAndCashEquivalentsAtCarryingValue"];
                    string tempCashAndShortTermInvestments = Data[0]["cashAndShortTermInvestments"];
                    string tempInventory = Data[0]["inventory"];
                    string tempCurrentNetReceivables = Data[0]["currentNetReceivables"];
                    string tempTotalNonCurrentAssets = Data[0]["totalNonCurrentAssets"];
                    string tempPropertyPlantEquipment = Data[0]["propertyPlantEquipment"];
                    string tempAccumulatedDepreciationAmortizationPPE = Data[0]["accumulatedDepreciationAmortizationPPE"];
                    string tempIntangibleAssets = Data[0]["intangibleAssets"];
                    string tempIntangibleAssetsExcludingGoodwill = Data[0]["intangibleAssetsExcludingGoodwill"];
                    string tempGoodwill = Data[0]["goodwill"];
                    string tempInvestments = Data[0]["investments"];
                    string tempLongTermInvestments = Data[0]["longTermInvestments"];
                    string tempShortTermInvestments = Data[0]["shortTermInvestments"];
                    string tempOtherCurrentAssets = Data[0]["otherCurrentAssets"];
                    string tempOtherNonCurrrentAssets = Data[0]["otherNonCurrrentAssets"];
                    string tempTotalLiabilities = Data[0]["totalLiabilities"];
                    string tempTotalCurrentLiabilities = Data[0]["totalCurrentLiabilities"];
                    string tempCurrentAccountsPayable = Data[0]["currentAccountsPayable"];
                    string tempDeferredRevenue = Data[0]["deferredRevenue"];
                    string tempCurrentDebt = Data[0]["currentDebt"];
                    string tempShortTermDebt = Data[0]["shortTermDebt"];
                    string tempTotalNonCurrentLiabilities = Data[0]["totalNonCurrentLiabilities"];
                    string tempCapitalLeaseObligations = Data[0]["capitalLeaseObligations"];
                    string tempLongTermDebt = Data[0]["longTermDebt"];
                    string tempCurrentLongTermDebt = Data[0]["currentLongTermDebt"];
                    string tempLongTermDebtNoncurrent = Data[0]["longTermDebtNoncurrent"];
                    string tempShortLongTermDebtTotal = Data[0]["shortLongTermDebtTotal"];
                    string tempOtherCurrentLiabilities = Data[0]["otherCurrentLiabilities"];
                    string tempOtherNonCurrentLiabilities = Data[0]["otherNonCurrentLiabilities"];
                    string tempTotalShareholderEquity = Data[0]["totalShareholderEquity"];
                    string tempTreasuryStock = Data[0]["treasuryStock"];
                    string tempRetainedEarnings = Data[0]["retainedEarnings"];
                    string tempCommonStock = Data[0]["commonStock"];
                    string tempCommonStockSharesOutstanding = Data[0]["commonStockSharesOutstanding"];
                    
                    BalanceSheet currentStock = new(
                        tempTicker,
                        tempFiscalDateEnding,
                        tempReportedCurrency,
                        tempTotalAssets,
                        tempTotalCurrentAssets,
                        tempCashAndCashEquivalentsAtCarryingValue,
                        tempCashAndShortTermInvestments,
                        tempInventory,
                        tempCurrentNetReceivables,
                        tempTotalNonCurrentAssets,
                        tempPropertyPlantEquipment,
                        tempAccumulatedDepreciationAmortizationPPE,
                        tempIntangibleAssets,
                        tempIntangibleAssetsExcludingGoodwill,
                        tempGoodwill,
                        tempInvestments,
                        tempLongTermInvestments,
                        tempShortTermInvestments,
                        tempOtherCurrentAssets,
                        tempOtherNonCurrrentAssets,
                        tempTotalLiabilities,
                        tempTotalCurrentLiabilities,
                        tempCurrentAccountsPayable,
                        tempDeferredRevenue,
                        tempCurrentDebt,
                        tempShortTermDebt,
                        tempTotalNonCurrentLiabilities,
                        tempCapitalLeaseObligations,
                        tempLongTermDebt,
                        tempCurrentLongTermDebt,
                        tempLongTermDebtNoncurrent,
                        tempShortLongTermDebtTotal,
                        tempOtherCurrentLiabilities,
                        tempOtherNonCurrentLiabilities,
                        tempTotalShareholderEquity,
                        tempTreasuryStock,
                        tempRetainedEarnings,
                        tempCommonStock,
                        tempCommonStockSharesOutstanding
                    );
                    parsedData.Add(currentStock);
                }

                return parsedData;
            }
        }
    }
}