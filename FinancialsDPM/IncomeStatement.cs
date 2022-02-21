namespace FinancialsDPM
{
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
    // Initial Implementation Done
    public class IncomeStatement
    {
        public string Ticker;
        public string fiscalDateEnding;
        public string reportedCurrency;
        public string grossProfit;
        public string totalRevenue;
        public string costOfRevenue;
        public string costofGoodsAndServicesSold;
        public string operatingIncome;
        public string sellingGeneralAndAdministrative;
        public string researchAndDevelopment;
        public string operatingExpenses;
        public string investmentIncomeNet;
        public string netInterestIncome;
        public string interestIncome;
        public string interestExpense;
        public string nonInterestIncome;
        public string otherNonOperatingIncome;
        public string depreciation;
        public string depreciationAndAmortization;
        public string incomerBeforeTax;
        public string incomeTaxExpense;
        public string interestAndDebtExpense;
        public string netIncomeFromContinuingOperations;
        public string comprehensiveIncomeNetOfTax;
        public string ebit;
        public string ebitda;
        public string netIncome; 
        
        public IncomeStatement(string ticker,string fiscalDateEnding, string reportedCurrency, string grossProfit, string totalRevenue,
            string costOfRevenue, string costofGoodsAndServicesSold, string operatingIncome,
            string sellingGeneralAndAdministrative, string researchAndDevelopment, string operatingExpenses,
            string investmentIncomeNet, string netInterestIncome, string interestIncome, string interestExpense,
            string nonInterestIncome, string otherNonOperatingIncome, string depreciation, string depreciationAndAmortization, string incomerBeforeTax,
            string incomeTaxExpense, string interestAndDebtExpense, string netIncomeFromContinuingOperations,
            string comprehensiveIncomeNetOfTax, string ebit, string ebitda, string netIncome)
        {
            Ticker = ticker ?? throw new ArgumentNullException(nameof(Ticker));
            this.fiscalDateEnding = fiscalDateEnding ?? throw new ArgumentNullException(nameof(fiscalDateEnding));
            this.reportedCurrency = reportedCurrency ?? throw new ArgumentNullException(nameof(reportedCurrency));
            this.grossProfit = grossProfit ?? throw new ArgumentNullException(nameof(grossProfit));
            this.totalRevenue = totalRevenue ?? throw new ArgumentNullException(nameof(totalRevenue));
            this.costOfRevenue = costOfRevenue ?? throw new ArgumentNullException(nameof(costOfRevenue));
            this.costofGoodsAndServicesSold = costofGoodsAndServicesSold ??
                                              throw new ArgumentNullException(nameof(costofGoodsAndServicesSold));
            this.operatingIncome = operatingIncome ?? throw new ArgumentNullException(nameof(operatingIncome));
            this.sellingGeneralAndAdministrative = sellingGeneralAndAdministrative ??
                                                   throw new ArgumentNullException(
                                                       nameof(sellingGeneralAndAdministrative));
            this.researchAndDevelopment = researchAndDevelopment ??
                                          throw new ArgumentNullException(nameof(researchAndDevelopment));
            this.operatingExpenses = operatingExpenses ?? throw new ArgumentNullException(nameof(operatingExpenses));
            this.investmentIncomeNet =
                investmentIncomeNet ?? throw new ArgumentNullException(nameof(investmentIncomeNet));
            this.netInterestIncome = netInterestIncome ?? throw new ArgumentNullException(nameof(netInterestIncome));
            this.interestIncome = interestIncome ?? throw new ArgumentNullException(nameof(interestIncome));
            this.interestExpense = interestExpense ?? throw new ArgumentNullException(nameof(interestExpense));
            this.nonInterestIncome = nonInterestIncome ?? throw new ArgumentNullException(nameof(nonInterestIncome));
            this.otherNonOperatingIncome = otherNonOperatingIncome ??
                                           throw new ArgumentNullException(nameof(otherNonOperatingIncome));
            this.depreciation = depreciation ?? throw new ArgumentNullException(nameof(depreciation));
            this.incomerBeforeTax = incomerBeforeTax ?? throw new ArgumentNullException(nameof(incomerBeforeTax));
            this.incomeTaxExpense = incomeTaxExpense ?? throw new ArgumentNullException(nameof(incomeTaxExpense));
            this.interestAndDebtExpense = interestAndDebtExpense ??
                                          throw new ArgumentNullException(nameof(interestAndDebtExpense));
            this.netIncomeFromContinuingOperations = netIncomeFromContinuingOperations ??
                                                     throw new ArgumentNullException(
                                                         nameof(netIncomeFromContinuingOperations));
            this.comprehensiveIncomeNetOfTax = comprehensiveIncomeNetOfTax ??
                                               throw new ArgumentNullException(nameof(comprehensiveIncomeNetOfTax));
            this.ebit = ebit ?? throw new ArgumentNullException(nameof(ebit));
            this.ebitda = ebitda ?? throw new ArgumentNullException(nameof(ebitda));
            this.netIncome = netIncome ?? throw new ArgumentNullException(nameof(netIncome));
            this.depreciationAndAmortization = depreciationAndAmortization;
        }

        public IncomeStatement(string ticker)
        {
            Ticker = ticker;
        }
    }

    public class GetIncomeStatementData
    {
        private static string api_key = "47BGPYJPFN4CEC20";

        public static async Task<List<string>> IncomeStatementRequest(ILogger ilogger, CancellationToken stoppingToken)
        {
            List<string> IncomeStatements = new();
            using HttpClient httpClient = new();
            string responseBody = String.Empty;
            foreach (string stock in StockList.SList)
            {

                try
                {
                    responseBody = await httpClient.GetStringAsync($"https://www.alphavantage.co/query?function=INCOME_STATEMENT&symbol={stock}&apikey={api_key}", stoppingToken);
                    ilogger.LogInformation("Successfully received info at: {Time}", DateTimeOffset.UtcNow);
                }
                catch (Exception e)
                {
                    ilogger.LogInformation("Exception thrown : {Exception}", e.ToString());
                }
                IncomeStatements.Add(responseBody);
                break;
            }

            return IncomeStatements;
        }

        public static List<IncomeStatement> ProcessIncomeStatementsData(List<string> dList)
        {
            List<IncomeStatement> parsedData = new();
            foreach (string response in dList)
            {
                var jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(response) ?? throw new InvalidOperationException();
                string tempTicker = jsonData["symbol"].GetString();
                var Data = JsonSerializer.Deserialize<List<Dictionary<String,String>>>(jsonData["annualReports"]);
                
                string tempFiscalDateEnding = Data[0]["fiscalDateEnding"];
                string tempReportedCurrency = Data[0]["reportedCurrency"];
                string tempGrossProfit = Data[0]["grossProfit"];
                string tempTotalRevenue = Data[0]["totalRevenue"];
                string tempCostOfRevenue = Data[0]["costOfRevenue"];
                string tempCostofGoodsAndServicesSold = Data[0]["costofGoodsAndServicesSold"];
                string tempOperatingIncome = Data[0]["operatingIncome"];
                string tempsellingGeneralAndAdministrative = Data[0]["sellingGeneralAndAdministrative"];
                string tempResearchAndDevelopment = Data[0]["researchAndDevelopment"];
                string tempOperatingExpenses = Data[0]["operatingExpenses"];
                string tempInvestmentIncomeNet = Data[0]["investmentIncomeNet"];
                string tempNetInterestIncome = Data[0]["netInterestIncome"];
                string tempInterestIncome = Data[0]["interestIncome"];
                string tempInterestExpense = Data[0]["interestExpense"];
                string tempNonInterestIncome = Data[0]["nonInterestIncome"];
                string tempOtherNonOperatingIncome = Data[0]["otherNonOperatingIncome"];
                string tempDepreciation = Data[0]["depreciation"];
                string tempDepreciationAndAmortization = Data[0]["depreciationAndAmortization"];
                string tempIncomeBeforeTax = Data[0]["incomeBeforeTax"];
                string tempIncomeTaxExpense = Data[0]["incomeTaxExpense"];
                string tempInterestAndDebtExpense = Data[0]["interestAndDebtExpense"];
                string tempNetIncomeFromContinuingOperations = Data[0]["netIncomeFromContinuingOperations"];
                string tempComprehensiveIncomeNetOfTax = Data[0]["comprehensiveIncomeNetOfTax"];
                string tempEBIT = Data[0]["ebit"];
                string tempEBITDA = Data[0]["ebitda"];
                string tempNetIncome = Data[0]["netIncome"];
                
                IncomeStatement currentStock = new IncomeStatement(   
                                                        tempTicker,
                                                        tempFiscalDateEnding,
                                                        tempReportedCurrency,
                                                        tempGrossProfit,
                                                        tempTotalRevenue,
                                                        tempCostOfRevenue,
                                                        tempCostofGoodsAndServicesSold,
                                                        tempOperatingIncome,
                                                        tempsellingGeneralAndAdministrative,
                                                        tempResearchAndDevelopment,
                                                        tempOperatingExpenses,
                                                        tempInvestmentIncomeNet,
                                                        tempNetInterestIncome,
                                                        tempInterestIncome,
                                                        tempInterestExpense,
                                                        tempNonInterestIncome,
                                                        tempOtherNonOperatingIncome,
                                                        tempDepreciation,
                                                        tempDepreciationAndAmortization,
                                                        tempIncomeBeforeTax,
                                                        tempIncomeTaxExpense,
                                                        tempInterestAndDebtExpense,
                                                        tempNetIncomeFromContinuingOperations,
                                                        tempComprehensiveIncomeNetOfTax,
                                                        tempEBIT,
                                                        tempEBITDA,
                                                        tempNetIncome);
                parsedData.Add(currentStock);
            }
            return parsedData;
        }
    }

}
}