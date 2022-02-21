using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FinancialsDPM
{
    public class CashFlow
    {
        public string Ticker;
        public string fiscalDateEnding;
        public string reportedCurrency;
        public string operatingCashflow;
        public string paymentsForOperatingActivities;
        public string proceedsFromOperatingActivities;
        public string changeInOperatingLiabilities;
        public string changeInOperatingAssets;
        public string depreciationDepletionAndAmortization;
        public string capitalExpenditures;
        public string changeInReceivables;
        public string changeInInventory;
        public string profitLoss;
        public string cashflowFromInvestment;
        public string cashflowFromFinancing;
        public string proceedsFromRepaymentsOfShortTermDebt;
        public string paymentsForRepurchaseOfCommonStock;
        public string paymentsForRepurchaseOfEquity;
        public string paymentsForRepurchaseOfPreferredStock;
        public string dividendPayout;
        public string dividendPayoutCommonStock;
        public string dividendPayoutPreferredStock;
        public string proceedsFromIssuanceOfCommonStock;
        public string proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet;
        public string proceedsFromIssuanceOfPreferredStock;
        public string proceedsFromRepurchaseOfEquity;
        public string proceedsFromSaleOfTreasuryStock;
        public string changeInCashAndCashEquivalents;
        public string changeInExchangeRate;
        public string netIncome;

        public CashFlow(string ticker, string fiscalDateEnding, string reportedCurrency, string operatingCashflow,
            string paymentsForOperatingActivities, string proceedsFromOperatingActivities,
            string changeInOperatingLiabilities, string changeInOperatingAssets,
            string depreciationDepletionAndAmortization, string capitalExpenditures, string changeInReceivables,
            string changeInInventory, string profitLoss, string cashflowFromInvestment, string cashflowFromFinancing,
            string proceedsFromRepaymentsOfShortTermDebt, string paymentsForRepurchaseOfCommonStock,
            string paymentsForRepurchaseOfEquity, string paymentsForRepurchaseOfPreferredStock, string dividendPayout,
            string dividendPayoutCommonStock, string dividendPayoutPreferredStock,
            string proceedsFromIssuanceOfCommonStock,
            string proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet,
            string proceedsFromIssuanceOfPreferredStock,
            string proceedsFromRepurchaseOfEquity, string proceedsFromSaleOfTreasuryStock,
            string changeInCashAndCashEquivalents, string changeInExchangeRate, string netIncome)
        {
            Ticker = ticker ?? throw new ArgumentNullException(nameof(ticker));
            this.fiscalDateEnding = fiscalDateEnding ?? throw new ArgumentNullException(nameof(fiscalDateEnding));
            this.reportedCurrency = reportedCurrency ?? throw new ArgumentNullException(nameof(reportedCurrency));
            this.operatingCashflow = operatingCashflow ?? throw new ArgumentNullException(nameof(operatingCashflow));
            this.paymentsForOperatingActivities = paymentsForOperatingActivities ??
                                                  throw new ArgumentNullException(
                                                      nameof(paymentsForOperatingActivities));
            this.proceedsFromOperatingActivities = proceedsFromOperatingActivities ??
                                                   throw new ArgumentNullException(
                                                       nameof(proceedsFromOperatingActivities));
            this.changeInOperatingLiabilities = changeInOperatingLiabilities ??
                                                throw new ArgumentNullException(nameof(changeInOperatingLiabilities));
            this.changeInOperatingAssets =
                changeInOperatingAssets ?? throw new ArgumentNullException(nameof(changeInOperatingAssets));
            this.depreciationDepletionAndAmortization = depreciationDepletionAndAmortization ??
                                                        throw new ArgumentNullException(
                                                            nameof(depreciationDepletionAndAmortization));
            this.capitalExpenditures =
                capitalExpenditures ?? throw new ArgumentNullException(nameof(capitalExpenditures));
            this.changeInReceivables =
                changeInReceivables ?? throw new ArgumentNullException(nameof(changeInReceivables));
            this.changeInInventory = changeInInventory ?? throw new ArgumentNullException(nameof(changeInInventory));
            this.profitLoss = profitLoss ?? throw new ArgumentNullException(nameof(profitLoss));
            this.cashflowFromInvestment =
                cashflowFromInvestment ?? throw new ArgumentNullException(nameof(cashflowFromInvestment));
            this.cashflowFromFinancing =
                cashflowFromFinancing ?? throw new ArgumentNullException(nameof(cashflowFromFinancing));
            this.proceedsFromRepaymentsOfShortTermDebt = proceedsFromRepaymentsOfShortTermDebt ??
                                                         throw new ArgumentNullException(
                                                             nameof(proceedsFromRepaymentsOfShortTermDebt));
            this.paymentsForRepurchaseOfCommonStock = paymentsForRepurchaseOfCommonStock ??
                                                      throw new ArgumentNullException(
                                                          nameof(paymentsForRepurchaseOfCommonStock));
            this.paymentsForRepurchaseOfEquity = paymentsForRepurchaseOfEquity ??
                                                 throw new ArgumentNullException(nameof(paymentsForRepurchaseOfEquity));
            this.paymentsForRepurchaseOfPreferredStock = paymentsForRepurchaseOfPreferredStock ??
                                                         throw new ArgumentNullException(
                                                             nameof(paymentsForRepurchaseOfPreferredStock));
            this.dividendPayout = dividendPayout ?? throw new ArgumentNullException(nameof(dividendPayout));
            this.dividendPayoutCommonStock = dividendPayoutCommonStock ??
                                             throw new ArgumentNullException(nameof(dividendPayoutCommonStock));
            this.dividendPayoutPreferredStock = dividendPayoutPreferredStock ??
                                                throw new ArgumentNullException(nameof(dividendPayoutPreferredStock));
            this.proceedsFromIssuanceOfCommonStock = proceedsFromIssuanceOfCommonStock ??
                                                     throw new ArgumentNullException(
                                                         nameof(proceedsFromIssuanceOfCommonStock));
            this.proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet =
                proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet ??
                throw new ArgumentNullException(nameof(proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet));
            this.proceedsFromIssuanceOfPreferredStock = proceedsFromIssuanceOfPreferredStock ??
                                                        throw new ArgumentNullException(
                                                            nameof(proceedsFromIssuanceOfPreferredStock));
            this.proceedsFromRepurchaseOfEquity = proceedsFromRepurchaseOfEquity ??
                                                  throw new ArgumentNullException(
                                                      nameof(proceedsFromRepurchaseOfEquity));
            this.proceedsFromSaleOfTreasuryStock = proceedsFromSaleOfTreasuryStock ??
                                                   throw new ArgumentNullException(
                                                       nameof(proceedsFromSaleOfTreasuryStock));
            this.changeInCashAndCashEquivalents = changeInCashAndCashEquivalents ??
                                                  throw new ArgumentNullException(
                                                      nameof(changeInCashAndCashEquivalents));
            this.changeInExchangeRate =
                changeInExchangeRate ?? throw new ArgumentNullException(nameof(changeInExchangeRate));
            this.netIncome = netIncome ?? throw new ArgumentNullException(nameof(netIncome));
        }

        public CashFlow(string ticker)
        {
            Ticker = ticker;
        }
    }

    public static class GetCashFlowData
    {
        private static string api_key = "47BGPYJPFN4CEC20";

        public static async Task<List<string>> CashFlowDataRequest(ILogger ilogger, CancellationToken stoppingToken)
        {
            List<string> cashFlows = new();
            using HttpClient httpClient = new();
            string responseBody = string.Empty;
            foreach (string stock in StockList.SList)
            {
                try
                {
                    responseBody = await httpClient.GetStringAsync(
                        $"https://www.alphavantage.co/query?function=CASH_FLOW&symbol={stock}&apikey={api_key}",
                        stoppingToken);
                    ilogger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                }
                catch (Exception e)
                {
                    ilogger.LogInformation("Exception thrown : {Exception}", e.ToString());
                }

                cashFlows.Add(responseBody);
                break;
            }

            return cashFlows;
        }

        public static List<CashFlow> ProcessCashFlowData(List<string> dList)
        {
            List<CashFlow> parsedData = new();
            foreach (string response in dList)
            {
                var jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(response) ??
                               throw new InvalidOperationException();
                string tempTicker = jsonData["symbol"].GetString();
                var Data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonData["annualReports"]);

                string tempFiscalDateEnding = Data[0]["fiscalDateEnding"];
                string tempReportedCurrency = Data[0]["reportedCurrency"];
                string tempOperatingCashflow = Data[0]["operatingCashflow"];
                string tempPaymentsForOperatingActivities = Data[0]["paymentsForOperatingActivities"];
                string tempProceedsFromOperatingActivities = Data[0]["proceedsFromOperatingActivities"];
                string tempChangeInOperatingLiabilities = Data[0]["changeInOperatingLiabilities"];
                string tempChangeInOperatingAssets = Data[0]["changeInOperatingAssets"];
                string tempDepreciationDepletionAndAmortization = Data[0]["depreciationDepletionAndAmortization"];
                string tempCapitalExpenditures = Data[0]["capitalExpenditures"];
                string tempChangeInReceivables = Data[0]["changeInReceivables"];
                string tempChangeInInventory = Data[0]["changeInInventory"];
                string tempProfitLoss = Data[0]["profitLoss"];
                string tempCashflowFromInvestment = Data[0]["cashflowFromInvestment"];
                string tempCashflowFromFinancing = Data[0]["cashflowFromFinancing"];
                string tempProceedsFromRepaymentsOfShortTermDebt = Data[0]["proceedsFromRepaymentsOfShortTermDebt"];
                string tempPaymentsForRepurchaseOfCommonStock = Data[0]["paymentsForRepurchaseOfCommonStock"];
                string tempPaymentsForRepurchaseOfEquity = Data[0]["paymentsForRepurchaseOfEquity"];
                string tempPaymentsForRepurchaseOfPreferredStock = Data[0]["paymentsForRepurchaseOfPreferredStock"];
                string tempDividendPayout = Data[0]["dividendPayout"];
                string tempDividendPayoutCommonStock = Data[0]["dividendPayoutCommonStock"];
                string tempDividendPayoutPreferredStock = Data[0]["dividendPayoutPreferredStock"];
                string tempProceedsFromIssuanceOfCommonStock = Data[0]["proceedsFromIssuanceOfCommonStock"];
                string tempProceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet =
                    Data[0]["proceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet"];
                string tempProceedsFromIssuanceOfPreferredStock = Data[0]["proceedsFromIssuanceOfPreferredStock"];
                string tempProceedsFromRepurchaseOfEquity = Data[0]["proceedsFromRepurchaseOfEquity"];
                string tempProceedsFromSaleOfTreasuryStock = Data[0]["proceedsFromSaleOfTreasuryStock"];
                string tempChangeInCashAndCashEquivalents = Data[0]["changeInCashAndCashEquivalents"];
                string tempChangeInExchangeRate = Data[0]["changeInExchangeRate"];
                string tempNetIncome = Data[0]["netIncome"];

                CashFlow currentStock = new(tempTicker,
                    tempFiscalDateEnding,
                    tempReportedCurrency,
                    tempOperatingCashflow,
                    tempPaymentsForOperatingActivities,
                    tempProceedsFromOperatingActivities,
                    tempChangeInOperatingLiabilities,
                    tempChangeInOperatingAssets,
                    tempDepreciationDepletionAndAmortization,
                    tempCapitalExpenditures,
                    tempChangeInReceivables,
                    tempChangeInInventory,
                    tempProfitLoss,
                    tempCashflowFromInvestment,
                    tempCashflowFromFinancing,
                    tempProceedsFromRepaymentsOfShortTermDebt,
                    tempPaymentsForRepurchaseOfCommonStock,
                    tempPaymentsForRepurchaseOfEquity,
                    tempPaymentsForRepurchaseOfPreferredStock,
                    tempDividendPayout,
                    tempDividendPayoutCommonStock,
                    tempDividendPayoutPreferredStock,
                    tempProceedsFromIssuanceOfCommonStock,
                    tempProceedsFromIssuanceOfLongTermDebtAndCapitalSecuritiesNet,
                    tempProceedsFromIssuanceOfPreferredStock,
                    tempProceedsFromRepurchaseOfEquity,
                    tempProceedsFromSaleOfTreasuryStock,
                    tempChangeInCashAndCashEquivalents,
                    tempChangeInExchangeRate,
                    tempNetIncome
                );
                parsedData.Add(currentStock);
            }
            return parsedData;
        }
    }
}